namespace Calculator.BusinessLogic.MessageBroker
{
    public interface IRabbitMqProducer<in T>
    {
        void Publish(T @event);
    }
}