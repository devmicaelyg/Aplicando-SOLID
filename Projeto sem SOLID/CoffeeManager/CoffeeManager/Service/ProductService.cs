using CoffeeManager.Interfaces;
using CoffeeManager.Model;

namespace CoffeeManager.Service
{
    //Single Responsibility Principle
    public class ProductService : IManagerService<Product>
    {
        private readonly List<Product> _products = new();

        public ProductService()
        { }

        public IEnumerable<Product> GetAll()
        {
            return _products;
        }

        public Product? GetById(Guid id)
        {
            if(id == Guid.Empty)
                throw new ArgumentNullException("id");

            return _products.FirstOrDefault(p => p.Id.Equals(id));
        }

        public void Add(Product product)
        {
            if (_products.Contains(product))
                throw new Exception("Esse produto já foi adicionado!");

            if(product.Name == null)
                throw new Exception("Não é possível adicionar um produto sem nome!");

            if(product.Price == 0)
                throw new Exception("Não é possível adicionar um produto sem preço!");

            product.Id = Guid.NewGuid();
            _products.Add(product);
        }

        public void Update(Product product)
        {
            if (product.Name == null)
                throw new Exception("Não é possível adicionar um produto sem nome!");

            if (product.Price == 0)
                throw new Exception("Não é possível adicionar um produto sem preço!");

            Product? productDb = GetById(product.Id);

            if(productDb is null)
                throw new Exception("Não foi encontrado o produto com esse identificador para editar!");

            productDb.Name = product.Name;
            productDb.Price = product.Price;
            productDb.Description = product.Description;
        }

        public void Delete(Guid id)
        {
            Product? productDb = GetById(id);

            if (productDb is null)
                throw new Exception("Não foi encontrado o produto com esse identificador para excluir!");

            _products.Remove(productDb);
        }
    }
}
