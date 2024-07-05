using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using ProfileService.Web.Interfaces;

namespace ProfileService.Web.Services;

public class RabbitMQPublisher : IRabbitMQPublisher
{
    private readonly IConfiguration _configuration;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMQPublisher(IConfiguration configuration)
    {
        _configuration = configuration;
        var factory = new ConnectionFactory { HostName = _configuration["RabbitMQ:HostName"] };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(
            queue: _configuration["RabbitMQ:QueueName"],
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }

    public void Publish(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);
        _channel.BasicPublish(exchange: "", routingKey: _configuration["RabbitMQ:QueueName"], basicProperties: null, body: body);
    }

    public void Dispose()
    {
        _channel.Close();
        _connection.Close();
    }
}
