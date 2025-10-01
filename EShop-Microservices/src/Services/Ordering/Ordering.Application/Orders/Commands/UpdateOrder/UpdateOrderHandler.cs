namespace Ordering.Application.Orders.Commands.UpdateOrder
{
   
    public class UpdateOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
    {
        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            var orderId = OrderId.Of(command.Order.Id);
            var order = await dbContext.Orders.FindAsync([orderId], cancellationToken);
            
            if (order == null)
            {
                throw new OrderNotFoundException(command.Order.Id);
            }

            UpdateOrderWithNewValues(order, command.Order);

            dbContext.Orders.Update(order);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateOrderResult(true);
        }

        public void UpdateOrderWithNewValues(Order order, OrderDTO orderDTO)
        {
            var shippingAddress = Address.Of(
                orderDTO.ShippingAddress.FirstName,
                orderDTO.ShippingAddress.LastName,
                orderDTO.ShippingAddress.EmailAddress,
                orderDTO.ShippingAddress.AddressLine,
                orderDTO.ShippingAddress.Country,
                orderDTO.ShippingAddress.State,
                orderDTO.ShippingAddress.ZipCode);
            var billingAddress = Address.Of(
                orderDTO.BillingAddress.FirstName,
                orderDTO.BillingAddress.LastName,
                orderDTO.BillingAddress.EmailAddress,
                orderDTO.BillingAddress.AddressLine,
                orderDTO.BillingAddress.Country,
                orderDTO.BillingAddress.State,
                orderDTO.BillingAddress.ZipCode);
            order.Update(
                OrderName.Of(orderDTO.OrderName),
                shippingAddress,
                billingAddress,
                Payment.Of(
                    orderDTO.Payment.CardName,
                    orderDTO.Payment.CardNumber,
                    orderDTO.Payment.Expiration,
                    orderDTO.Payment.Cvv,
                    orderDTO.Payment.PaymentMethod),
                orderDTO.Status);
        }
    }
}