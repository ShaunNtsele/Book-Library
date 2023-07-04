using Book_Library.Auth;

namespace Book_Library.Data 
{
     public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ApplicationDbContext _appDbContext;
        public RepositoryBase(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Create(T entity)
        {
            _appDbContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _appDbContext.Set<T>().Remove(entity);
        }

        public IEnumerable<T> Read()
        {
            return _appDbContext.Set<T>();
        }

        public void Update(T entity)
        {
            _appDbContext.Set<T>().Update(entity);
        }

          public T GetById(Guid id)
        {
             
            return _appDbContext.Set<T>().Find(id);
           
        }
    }
}