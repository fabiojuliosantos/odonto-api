using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Odonto.Application.DTO;
using Odonto.Application.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

public class RabbitMqMessageConsumer : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private IConnection _connection;
    private IChannel _channel;

    public RabbitMqMessageConsumer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
        };

        // Criar conexão e canal de forma assíncrona
        _connection = await factory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();

        await _channel.QueueDeclareAsync(queue: "odonto.consultas.marcar", false, false, false, arguments: null);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (_, evt) =>
        {
            try
            {
                // Deserializar a mensagem recebida
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                var consulta = JsonSerializer.Deserialize<CadastrarConsultaDTO>(content);

                // Resolver serviço e processar a mensagem
                using (var scope = _serviceProvider.CreateScope())
                {
                    var consultaService = scope.ServiceProvider.GetRequiredService<IConsultaService>();
                    await consultaService.CadastrarConsulta(consulta);
                }

                // Confirmar o processamento da mensagem
                await _channel.BasicAckAsync(evt.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                // Tratar falhas e rejeitar a mensagem
                await _channel.BasicNackAsync(evt.DeliveryTag, false, false);
                Console.WriteLine($"Erro ao processar mensagem: {ex.Message}");
            }
        };

        await _channel.BasicConsumeAsync(
            queue: "odonto.consultas.marcar",
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
