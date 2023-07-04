namespace Book_Library.Data
{
    public interface IRepositoryWrapper
    {
        IAuthorRepository Author { get; }
        IBookRepository Book {get;}
        void Save();
    }
}