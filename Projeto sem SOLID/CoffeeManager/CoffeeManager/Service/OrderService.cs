using CoffeeManager.Interfaces;
using CoffeeManager.Model;

namespace CoffeeManager.Service
{
    //Single Responsibility Principle
    //Interface Segregation Principle - ISP
    public class OrderService : IManagerService<Order>
    {
        private readonly List<Order> _orders = new();

        public void Add(Order entity)
        {
            if(entity is null)
                throw new ArgumentNullException(nameof(entity));

            if (entity.) { }

        }
    
        /// <summary>
        /// Não será possivel apagar um pedido, somente atualizar o status dele
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public Order? GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Order entity)
        {
            throw new NotImplementedException();
        }
    }
}
