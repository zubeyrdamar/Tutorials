using ShopApp.Business.Abstract;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entities;

namespace ShopApp.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoryManager(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public List<Category> List()
        {
            return categoryRepository.List().ToList();
        }

        public Category Read(int id)
        {
            return categoryRepository.Read(id);
        }

        public void Create(Category category)
        {
            categoryRepository.Create(category);
        }

        public void Update(Category category)
        { 
            categoryRepository.Update(category);
        }

        public void Delete(Category category)
        {
            categoryRepository.Delete(category);
        }
    }
}