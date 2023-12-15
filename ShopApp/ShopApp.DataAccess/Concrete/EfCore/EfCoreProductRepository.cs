using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess.Abstract;
using ShopApp.DataAccess.Concrete.EfCore;
using ShopApp.Entities;

namespace ShopApp.DataAccess.Concrete.EFCore
{
    public class EfCoreProductRepository : EfCoreGenericRepository<Product, ShopDbContext>, IProductRepository
    {
        public Product Details(int id)
        {
            using (var context = new ShopDbContext())
            {
                return context.Products
                                        .Where(p => p.Id == id)
                                        .Include(p => p.ProductCategories)
                                        .ThenInclude(pc => pc.Category)
                                        .FirstOrDefault();
            }
        }
    }
}
