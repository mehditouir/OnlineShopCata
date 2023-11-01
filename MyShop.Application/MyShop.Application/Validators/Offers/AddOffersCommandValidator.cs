using FluentValidation;
using MyShop.Application.Commands.Offers;

namespace MyShop.Application.Validators.Offers
{
    public class AddOffersCommandValidator : AbstractValidator<AddOffersCommand>
    {
        public AddOffersCommandValidator()
        {
            RuleFor(x => x).NotNull();
            RuleFor(x => x.Offer).NotNull();
            RuleFor(x => x.Offer.ProductName).NotEmpty();
            RuleFor(x => x.Offer.ProductBrand).NotEmpty();
            RuleFor(x => x.Offer.Price).GreaterThan(0);
            RuleFor(x => x.Offer.Quantity).GreaterThan(0);
        }
    }
}
