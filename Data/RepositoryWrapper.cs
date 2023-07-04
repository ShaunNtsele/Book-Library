using Book_Library.Auth;

namespace Book_Library.Data
{
    public class RepositoryWrapper: IRepositoryWrapper
    {
        private ApplicationDbContext _appDbContext;
        private IAuthorRepository _author;

        private IBookRepository _book;

        public RepositoryWrapper(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IAuthorRepository Author
        {
            get
            {
                if (_author == null)
                {
                    _author = new AuthorRepository(_appDbContext);
                }

                return _author;
            }
        }

        public IBookRepository Book
        {
            get
            {
                if(_book == null)
                {
                    _book = new BookRepository(_appDbContext);
                }
                return _book;
            }
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    }
}