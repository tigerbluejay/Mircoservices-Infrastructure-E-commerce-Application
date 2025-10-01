

namespace Ordering.Application.Orders.EventHandlers
{
    public class OrderUpdatedEventHandler(ILogger<OrderUpdatedEventHandler> logger)
        : INotificationHandler<OrderUpdatedEvent>
    {
        public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("OrderUpdatedEvent domain event handled: {DomainEvent}", notification.GetType().Name);
            throw new NotImplementedException();
        }
    }
}
