using KutuphaneSatis.Models.Abstract;
using System.Linq.Expressions;

namespace KutuphaneSatis.Repositories.Abstract
{
    public interface IGenericRepository<T> where T : BaseEntities
    {



        public IEnumerable<T> GetAll();
        T GetByID(int id, params Expression<Func<T, object>>[] includes);
        public void Delete(int id);
        public void Create(T entity);
        public void Update(T entity);

     

    }
}
