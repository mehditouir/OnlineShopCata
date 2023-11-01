namespace MyShop.Domain.Models
{
    public record Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Size { get; set; }
    }
}
