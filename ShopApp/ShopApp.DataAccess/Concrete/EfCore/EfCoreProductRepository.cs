using ShopApp.DataAccess.Abstract;
using ShopApp.DataAccess.Concrete.EfCore;
using ShopApp.Entities;

namespace ShopApp.DataAccess.Concrete.EFCore
{
    internal class EfCoreProductRepository : EfCoreGenericRepository<Product, ShopDbContext>, IProductRepository
    {
        
    }
}
