using ShopApp.DataAccess.Abstract;
using ShopApp.DataAccess.Concrete.EfCore;
using ShopApp.Entities;

namespace ShopApp.DataAccess.Concrete.EFCore
{
    public class EfCoreProductRepository : EfCoreGenericRepository<Product, ShopDbContext>, IProductRepository
    {
        
    }
}
