namespace MyShop.Domain.Models
{
    public record Stock
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }
}
