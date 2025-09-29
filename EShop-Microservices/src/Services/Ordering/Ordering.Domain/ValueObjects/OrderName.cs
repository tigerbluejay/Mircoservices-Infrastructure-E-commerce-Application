
namespace Ordering.Domain.ValueObjects
{
    public record OrderName
    {
        private const int DefaultLength = 5;
        public string Value { get; }
        private OrderName(string value ) => Value = value;

        public static OrderName Of(string value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainException("OrderName value cannot be empty.");
            }
            if (value.Length != DefaultLength)
            {
                throw new DomainException($"OrderName value must be {DefaultLength} characters long.");
            }
            return new OrderName(value);
        }
    }
}
