namespace Domain
{
    public class Product
    {
        public ProductId Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public Money Price { get; private set; }
        public Sku Sku { get; private set; }

    }

    public record Money(string Currency, decimal Amount);

    public record ProductId(Guid Value);
}
