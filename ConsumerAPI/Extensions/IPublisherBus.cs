using MassTransit;

namespace ConsumerAPI.Extensions
{
    public interface IPublisherBus
    {
        Task PublishAsync<T>(T message, CancellationToken cancellationToken) where T : class;
    }

    public class PublisherBus : IPublisherBus
    {
        private readonly IBus _publisher;

        public PublisherBus(IBus publisher)
        {
            _publisher = publisher;
        }

        public Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class
        {
            return _publisher.Publish(message, cancellationToken);
        }
    }
}
