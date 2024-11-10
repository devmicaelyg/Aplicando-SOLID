using CoffeeManager.Model;

namespace CoffeeManager.Repository
{
    public class ProductRepository
    {
        private readonly List<Product> _products = new();

        public ProductRepository()
        {  }

        public IEnumerable<Product> GetAll()
        {
            return _products;
        }

        public Product? GetById(Guid id)
        {
            return  _products.FirstOrDefault(p => p.Id.Equals(id));
        }

        public void Add(Product product)
        {
            product.Id = Guid.NewGuid();
            _products.Add(product);
        }
    }
}
