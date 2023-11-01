using MyShop.Domain.Models;

namespace MyShop.Domain.Mappers
{
    public static class OfferMappers
    {
        public static Product ToProduct(this Offer item)
        {
            return new Product
            {
                Id = item.ProductId,
                Brand = item.ProductBrand,
                Name = item.ProductName,
                Size = item.ProductSize
            };
        }

        public static Stock ToStock(this Offer item, int productId)
        {
            return new Stock
            {
                ProductId = productId,
                Amount = item.Quantity
            };
        }

        public static Price ToPrice(this Offer item, int productId)
        {
            return new Price
            {
                ProductId = productId,
                Amount = item.Price
            };
        }
    }
}
