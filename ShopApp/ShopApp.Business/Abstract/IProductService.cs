using ShopApp.Entities;

namespace ShopApp.Business.Abstract
{
    public interface IProductService
    {
        List<Product> List();
        Product Read(int id);
        void Create(Product product);
        void Update(Product product);
        void Delete(Product product);
    }
}
