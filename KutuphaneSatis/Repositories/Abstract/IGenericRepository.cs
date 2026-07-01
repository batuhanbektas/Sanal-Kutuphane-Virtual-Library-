using KutuphaneSatis.Models.Abstract;

namespace KutuphaneSatis.Repositories.Abstract
{
    public interface IGenericRepository<T> where T : BaseEntities
    {



        public IEnumerable<T> GetAll();
        public T GetByID(int id);
        public void Delete(int id);
        public void Create(T entity);
        public void Update(T entity);

     

    }
}
