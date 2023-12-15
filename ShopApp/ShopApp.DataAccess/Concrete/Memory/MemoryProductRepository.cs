using ShopApp.DataAccess.Abstract;
using ShopApp.Entities;
using System.Linq.Expressions;

namespace ShopApp.DataAccess.Concrete.Memory
{
    public class MemoryProductRepository : IProductRepository
    {
        public IEnumerable<Product> List(Expression<Func<Product, bool>>? filter = null)
        {
            var products = new List<Product>()
            {
                new Product { Id = 1, Name = "Iphone S10", Price = 500},
                new Product { Id = 2, Name = "Iphone S11", Price = 700},
                new Product { Id = 3, Name = "Iphone S12", Price = 1000},
                new Product { Id = 4, Name = "Samsung G1", Price = 400},
            };

            return products;
        }

        public void Create(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public Product Filter(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Product Read(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
