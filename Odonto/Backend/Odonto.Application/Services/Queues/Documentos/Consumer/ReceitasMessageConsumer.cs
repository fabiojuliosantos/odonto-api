using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Odonto.API.DTOs.Documentos;
using Odonto.Application.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StackExchange.Redis;
using System.Text.Json;
using System.Text;

namespace Odonto.Application.Services.Queues.Documentos.Consumer;

public class ReceitasMessageConsumer : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IConnectionMultiplexer _redis;
    private IConnection _connection;
    private IChannel _channel;

    public ReceitasMessageConsumer(IServiceProvider serviceProvider, IConnectionMultiplexer redis)
    {
        _serviceProvider = serviceProvider;
        _redis = redis;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        ConnectionFactory factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
        };

        _connection = await factory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();

        await _channel.QueueDeclareAsync(queue: "odonto.documentos.receita", false, false, false, arguments: null);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        var redisDb = _redis.GetDatabase();

        consumer.ReceivedAsync += async (_, evt) =>
        {
            string content = Encoding.UTF8.GetString(evt.Body.ToArray());

            ReceitaDTO receita = JsonSerializer.Deserialize<ReceitaDTO>(content);

            try
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    IDocumentosService documentosService = scope.ServiceProvider.GetService<IDocumentosService>();

                    byte[] receitaRetorno = await documentosService.GerarReceita(receita);

                    var base64 = Convert.ToBase64String(receitaRetorno);

                    redisDb.StringSet($"receita:{receita.Id}:status", "Finalizado");

                    redisDb.StringSet($"receita:{receita.Id}:result", base64);
                }
                await _channel.BasicAckAsync(evt.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                redisDb.StringSet($"receita:{receita.Id}:status", "Falha");

                await _channel.BasicNackAsync(evt.DeliveryTag, false, false);

                Console.WriteLine($"Erro ao processar mensagem: {ex.Message}");
            }
        };
        await _channel.BasicConsumeAsync(
            queue: "odonto.documentos.receita",
            autoAck: false,
            consumer: consumer);

        stoppingToken.Register(() =>
        {
            _channel.CloseAsync();
            _connection.CloseAsync();
        });
    }
    public override void Dispose()
    {
        _channel?.CloseAsync();
        _connection?.CloseAsync();
        base.Dispose();
    }
}

