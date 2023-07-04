using Book_Library.Models;

namespace Book_Library.Data
{
    public interface IBookRepository: IRepositoryBase<Book>
    {


     Book GetBookWithAuthorDetails(Guid id); 
     IEnumerable<Book> GetBooksWithAuthorDetails();  
    }
}