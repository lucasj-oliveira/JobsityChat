using StockApi.Domain;

namespace StockApi.RabbitMQ
{
    public interface IRabbitMQProducer
    {
        public void SendMessage<T>(StockResponse message);
    }
}