using Book_Library.Auth;
using Book_Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Book_Library.Data
{
     public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext appDbContext)
            : base(appDbContext)
        {
        }

 
        public Book GetBookWithAuthorDetails(Guid id)
       {
            return _appDbContext.Books
                .Include(a=> a.Author)
                .FirstOrDefault(bookAuthor=>bookAuthor.Author.AuthorId == id);
        } 

         public IEnumerable<Book> GetBooksWithAuthorDetails()
        {
            return _appDbContext.Books
            .Include(a=> a.Author);
        }
    }
}