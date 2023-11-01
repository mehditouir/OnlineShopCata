using MyShop.Api.Endpoints.Responses.Offers;
using MyShop.Domain.Models;
using MyShop.Domain.ViewModels;

namespace MyShop.Api.Endpoints.Mappers.Offers
{
    public static class DomainToContractMappers
    {
        public static OfferAddResponse ToAddContract(this Offer offer)
        {
            if (offer is null)
                return new OfferAddResponse();

            return new OfferAddResponse
            {
                Price = offer.Price,
                ProductBrand = offer.ProductBrand,
                ProductName = offer.ProductName,
                ProductId = offer.ProductId,
                ProductSize = offer.ProductSize,
                Quantity = offer.Quantity
            };
        }

        public static OfferUpdateResponse ToUpdateContract(this Offer offer)
        {
            if (offer is null)
                return new OfferUpdateResponse();

            return new OfferUpdateResponse
            {
                Price = offer.Price,
                ProductBrand = offer.ProductBrand,
                ProductName = offer.ProductName,
                ProductId = offer.ProductId,
                ProductSize = offer.ProductSize,
                Quantity = offer.Quantity
            };
        }

        public static IEnumerable<OfferVm> ToAllContract(this IEnumerable<Offer> offer)
        {
            return offer.Select(item => item.ToContract());
        }

        private static OfferVm ToContract(this Offer offer)
        {
            return new OfferVm
            {
                Price = offer.Price,
                ProductBrand = offer.ProductBrand,
                ProductName = offer.ProductName,
                ProductId = offer.ProductId,
                ProductSize = offer.ProductSize,
                Quantity = offer.Quantity
            };
        }
    }
}
