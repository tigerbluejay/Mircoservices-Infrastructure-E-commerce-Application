﻿
using MediatR.NotificationPublishers;

namespace Ordering.Domain.Models
{
    public class Order : Aggregate<OrderId>
    {
        private readonly List<OrderItem> _orderItems = new();
        public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
        public CustomerId CustomerId { get; private set; } = default!;
        public OrderName OrderName { get; private set; } = default!;
        public Address ShippingAddress { get; private set; } = default!;
        public Address BillingAddress { get; private set; } = default!;
        public Payment Payment { get; private set; } = default!;
        public OrderStatus Status { get; private set; } = OrderStatus.Pending;
        public decimal TotalPrice
        {
            get => OrderItems.Sum(item => item.Price * item.Quantity);
            private set { }

        }

        public static Order Create(OrderId id, CustomerId customerId, OrderName orderName, 
            Address shippingAddress, Address billingAddress, Payment payment)
        {
            var order = new Order
            {
                Id = OrderId.Of(Guid.NewGuid()),
                CustomerId = customerId,
                OrderName = orderName,
                ShippingAddress = shippingAddress,
                BillingAddress = billingAddress,
                Payment = payment,
                Status = OrderStatus.Pending,
            };

            order.AddDomainEvent(new OrderCreatedEvent(order));

            return order;
        }

        public void Update(OrderName orderName, Address shippingAddress, Address billingAddress, Payment payment, OrderStatus status)
        {
            OrderName = orderName;
            ShippingAddress = shippingAddress;
            BillingAddress = billingAddress;
            Payment = payment;
            Status = status;

            AddDomainEvent(new OrderUpdatedEvent(this));
        }

        public void Add(ProductId productId, int quantity, decimal price)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
 
            var orderItem = new OrderItem(this.Id, productId, quantity, price);

            _orderItems.Add(orderItem); // encapsulate the order items into the aggregate
        }

        public void Remove(ProductId productId)
        {
            var orderItem = _orderItems.FirstOrDefault(oi => oi.ProductId == productId);
            if (orderItem != null)
            {
                _orderItems.Remove(orderItem);
            }
        }
    }
}