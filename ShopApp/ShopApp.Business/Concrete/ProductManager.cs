using ShopApp.Business.Abstract;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entities;

namespace ShopApp.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductRepository productRepository;
        public ProductManager(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public List<Product> List()
        {
            return productRepository.List().ToList();
        }

        public Product Read(int id)
        {
            return productRepository.Read(id);
        }

        public Product Details(int id)
        {
            return productRepository.Details(id);
        }

        public void Create(Product product)
        {
            productRepository.Create(product);
        }

        public void Update(Product product)
        {
            productRepository.Update(product);
        }

        public void Delete(Product product)
        {
            productRepository.Delete(product);
        }
    }
}
