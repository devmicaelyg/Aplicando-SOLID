namespace CoffeeManager.Model
{
    public class Order
    {
        public Order(Guid id, int number, StatusType status, double price, DateTime createdDate, DateTime lastUpdatedDate, ICollection<Product> products)
        {
            Id = id;
            Number = number;
            Status = status;
            Price = price;
            CreatedDate = createdDate;
            LastUpdatedDate = lastUpdatedDate;
            Products = products;
        }

        private Guid Id { get; set; }

        private int Number { get; set; }

        private StatusType Status { get; set; }

        private double Price { get; set; }

        private double? DiscountedPrice { get; set; }   

        private DateTime CreatedDate { get; set; } = DateTime.Now;

        private DateTime LastUpdatedDate { get; set; }

        private ICollection<Product> Products { get; set; }
    }
}
