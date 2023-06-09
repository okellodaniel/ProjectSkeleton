namespace Domain
{
    public class Order
    {
        public readonly HashSet<LineItem> _lineItems = new ();
        private Order(){}
        public OrderId Id { get; private set; }
        public CustomerId CustomerId { get; private set; }

        public static Order Create(Customer customer)
        {
            var order = new Order()
            {
                Id = new OrderId(Guid.NewGuid()),
                CustomerId = customer.Id,
            };

            return order;
        }
        public void Add(Product product)
        {
            var lineItem = new LineItem(new LineItemId(Guid.NewGuid()),new OrderId(Guid.NewGuid()), product.Id, product.Price);
            _lineItems.Add(lineItem);
        }
    }
}
