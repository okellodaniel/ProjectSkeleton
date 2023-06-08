namespace Domain
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public Money Price { get; private set; }
        public Sku Sku { get; private set; }

    }

    public record Money(string Currency, decimal Amount);

    public record Sku
    {
        private const int DefaultLength = 15;
        private Sku(string value) => Value = value;
        public string Value { get; init; }
        public static Sku? Create(string value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            if (value.Length != DefaultLength) return null;
            return new Sku(value);
        }
    }
}
