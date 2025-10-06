
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket
{
    // public record CheckoutBasketCommand(BasketCheckoutDTO BasketCheckoutDTO) 
    //     : ICommand<CheckoutBasketResult>;

    public class CheckoutBasketCommand : ICommand<CheckoutBasketResult>
    {
        public BasketCheckoutDTO BasketCheckoutDTO { get; set; } = default!;
    }

    public record CheckoutBasketResult(bool IsSuccess);

    public class CheckoutBasketCommandValidator
        : AbstractValidator<CheckoutBasketCommand>
    {
        public CheckoutBasketCommandValidator()
        {
            RuleFor(x => x.BasketCheckoutDTO).NotNull().WithMessage("BasketCheckoutDTO is required");
            RuleFor(x => x.BasketCheckoutDTO.UserName).NotEmpty().WithMessage("UserName is required");
        }
    }

    public class CheckoutBasketCommandHandler(IBasketRepository basketRepository,
        IPublishEndpoint publishEndpoint)
        : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
    {
        public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
        {
            var basket = await basketRepository.GetBasket(command.BasketCheckoutDTO.UserName, cancellationToken);
            if (basket == null)
            {
                return new CheckoutBasketResult(false);
            }

            var eventMessage = command.BasketCheckoutDTO.Adapt<BasketCheckoutEvent>();
            eventMessage.TotalPrice = basket.TotalPrice;

            // publish event to RabbitMQ
            await publishEndpoint.Publish(eventMessage, cancellationToken);

            await basketRepository.DeleteBasket(command.BasketCheckoutDTO.UserName, cancellationToken);

            return new CheckoutBasketResult(true);

        }
    }
}
