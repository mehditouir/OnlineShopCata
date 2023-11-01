namespace MyShop.Domain.Models
{
    public record Offer
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductBrand { get; set; }
        public string ProductSize { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public static Offer Build(Product product, Price price, Stock stock)
        {
            return new Offer
            {
                Price = price.Amount,
                Quantity = stock.Amount,
                ProductBrand = product.Brand,
                ProductSize = product.Size,
                ProductName = product.Name,
                ProductId = product.Id
            };
        }
    }
}
