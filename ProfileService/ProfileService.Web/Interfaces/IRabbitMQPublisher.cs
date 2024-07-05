namespace ProfileService.Web.Interfaces;

public interface IRabbitMQPublisher
{
    void Publish(string message);
}