using ShopApp.Entities;

namespace ShopApp.DataAccess.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
        public Product Details(int id);
    }
}
