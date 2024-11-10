namespace CoffeeManager.Model
{
    public class Product
    {
        public Product(Guid id, string name, string? description, decimal price)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; } = 0;
    }
}
