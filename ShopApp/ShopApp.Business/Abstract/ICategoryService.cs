using ShopApp.Entities;

namespace ShopApp.Business.Abstract
{
    public interface ICategoryService
    {
        List<Category> List();
        Category Read(int id);
        void Create(Category category);
        void Update(Category category);
        void Delete(Category category);
    }
}
