

using BuildingBlocks.Messaging.Events;
using MassTransit;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.Application.Orders.EventHandlers.Integration
{
    public class BasketCheckoutEventHandler(ISender sender, ILogger<BasketCheckoutEventHandler> logger)
        : IConsumer<BasketCheckoutEvent>
    {
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
           logger.LogInformation("BasketCheckoutEvent integration event handled: {IntegrationEvent}", context.Message.GetType().Name);

           var command = MapToCreateOrderCommand(context.Message);
           await sender.Send(command);

        }

        private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
        {
            var addressDTO = new AddressDTO(message.FirstName, message.LastName,
                message.EmailAddress, message.AddressLine, message.Country,
                message.State, message.ZipCode);

            var paymentDTO = new PaymentDTO(message.CardName, message.CardNumber,
                message.Expiration, message.CVV, message.PaymentMethod);
            
            var orderId = Guid.NewGuid(); // Generate a new Order ID    

            var orderDTO = new OrderDTO(
                Id: orderId,
                CustomerId: message.CustomerId,
                OrderName: message.UserName,
                ShippingAddress: addressDTO,
                BillingAddress: addressDTO, // Assuming billing address is same as shipping
                Payment: paymentDTO,
                Status: Ordering.Domain.Enum.OrderStatus.Pending,
                OrderItems:
                [
                    // the hardcoded Guid is the productId from pre seeded data
                    new OrderItemDTO(orderId, new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"), 2, 500),
                    new OrderItemDTO(orderId, new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"), 2, 500)
                ]);

        return new CreateOrderCommand(orderDTO);    
        }
    }
}
