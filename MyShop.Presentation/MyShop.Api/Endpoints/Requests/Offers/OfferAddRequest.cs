namespace MyShop.Api.Endpoints.Requests.Offers
{
    public class OfferAddRequest
    {
        public string ProductName { get; set; }
        public string ProductBrand { get; set; }
        public string ProductSize { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
