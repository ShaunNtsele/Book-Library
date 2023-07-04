namespace Book_Library.Data
{
    public interface IRepositoryBase<T>
    {
        
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        IEnumerable<T> Read();
        T GetById(Guid id);
    }
}