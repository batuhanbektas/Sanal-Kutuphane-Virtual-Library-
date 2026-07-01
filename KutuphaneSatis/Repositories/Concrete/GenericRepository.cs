using KutuphaneSatis.Data;
using KutuphaneSatis.Models.Abstract;
using KutuphaneSatis.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace KutuphaneSatis.Repositories.Abstract
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntities
    {


        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;



        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }




        public IEnumerable<T> GetAll()
        {

            return _dbSet.ToList();


        }
        public T GetByID(int id)
        {
            return _dbSet.Find(id);
        }
        

        public void Delete(int id)
        {
            var entityToDelete = _dbSet.Find(id);

            if (entityToDelete != null) 
            {
                _dbSet.Remove(entityToDelete);
                _context.SaveChanges();
   
            
            }

        }
        public void Create(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();


        }
        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();

        }



    }
}
