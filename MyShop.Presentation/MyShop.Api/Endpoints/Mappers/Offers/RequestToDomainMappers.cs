using MyShop.Api.Endpoints.Requests.Offers;
using MyShop.Domain.Models;

namespace MyShop.Api.Endpoints.Mappers.Offers
{
    public static class RequestToDomainMappers
    {
        public static Offer ToDomain(this OfferAddRequest request)
        {
            return new Offer
            {
                ProductName = request.ProductName,
                Price = request.Price,
                ProductBrand = request.ProductBrand,
                ProductSize = request.ProductSize,
                Quantity = request.Quantity
            };
        }

        public static Offer ToDomain(this OfferUpdateRequest request)
        {
            return new Offer
            {
                ProductId = request.ProductId,
                ProductName = request.ProductName,
                Price = request.Price,
                ProductBrand = request.ProductBrand,
                ProductSize = request.ProductSize,
                Quantity = request.Quantity
            };
        }
    }
}
