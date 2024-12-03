using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Odonto.API.DTOs.Documentos;
using Odonto.Application.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StackExchange.Redis;
using System.Text;
using System.Text.Json;

namespace Odonto.Application.Services.Queues.Documentos.Consumer;

public class AtestadosMessageConsumer : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IConnectionMultiplexer _redis;
    private IConnection _connection;
    private IChannel _channel;
    public AtestadosMessageConsumer(IServiceProvider serviceProvider, IConnectionMultiplexer redis)
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

        await _channel.QueueDeclareAsync(queue: "odonto.documentos.atestado", false, false, false, arguments: null);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        var redisDb = _redis.GetDatabase();

        consumer.ReceivedAsync += async (_, evt) =>
        {
            string content = Encoding.UTF8.GetString(evt.Body.ToArray());
            AtestadoDTO atestado = JsonSerializer.Deserialize<AtestadoDTO>(content);

            try
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    IDocumentosService documentosService = scope.ServiceProvider.GetService<IDocumentosService>();

                    byte[] atestadoRetorno = await documentosService.GerarAtestado(atestado);

                    var base64 = Convert.ToBase64String(atestadoRetorno);

                    redisDb.StringSet($"atestado:{atestado.Id}:status", "Finalizado");

                    redisDb.StringSet($"atestado:{atestado.Id}:result", base64);
                }
                await _channel.BasicAckAsync(evt.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                redisDb.StringSet($"atestado:{atestado.Id}:status", "Falha");

                await _channel.BasicNackAsync(evt.DeliveryTag, false, false);

                Console.WriteLine($"Erro ao processar mensagem: {ex.Message}");
            }
        };
        await _channel.BasicConsumeAsync(
            queue: "odonto.documentos.atestado",
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
