using Book_Library.Auth;
using Book_Library.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
namespace Book_Library.Data 
{
public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorRepository(ApplicationDbContext appDbContext)
            : base(appDbContext)
        {
        }

      
    }

}