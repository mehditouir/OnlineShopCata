namespace MyShop.Domain.Models
{
    public record Price
    {
        public int ProductId { get; set; }
        public decimal Amount { get; set; }
    }
}
