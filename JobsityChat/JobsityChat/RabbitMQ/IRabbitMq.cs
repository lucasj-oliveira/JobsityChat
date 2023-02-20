namespace JobsityChat.RabbitMq
{
    public interface IRabbitMq
    {
        public void SendMessage<T>(T message);
    }
}