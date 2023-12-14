using System.Linq.Expressions;

namespace ShopApp.DataAccess.Abstract
{
    public interface IRepository<T>
    {
        IEnumerable<T> List(Expression<Func<T, bool>>? filter = null);
        T Read(int id);
        T Filter(Expression<Func<T, bool>> filter);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
