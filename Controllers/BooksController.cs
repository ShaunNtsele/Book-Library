using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Book_Library.Data;
using Book_Library.Models;

namespace Book_Library.Controllers
{
[Authorize]
[Route("[controller]")]
[ApiController]
public class BooksController : ControllerBase
{


    private readonly IRepositoryWrapper _repo;
    private readonly UserManager<IdentityUser> _userManager;

    public BooksController(IRepositoryWrapper repo, UserManager<IdentityUser> userManager)
    {
        _repo = repo;
        _userManager = userManager;
    }

    [HttpPost]
    [Route("{authorId}")]
    //create new book
    public IActionResult Create(Book book, Guid authorId)
    {
        if (ModelState.IsValid)
        {
            var currentUser = _userManager.FindByNameAsync(User.Identity.Name);
            var userId = currentUser.Result.Id;
            _repo.Book.Create(book);
            book.CreatedBy = Guid.Parse(userId); 
            var author = _repo.Author.GetById(authorId);
            book.Author = author;
            _repo.Save();


            return Ok(book);
        }
        else
        {
            return StatusCode(StatusCodes.Status400BadRequest);
        }
    }

    [HttpGet]
   
    //return list of all books
    public IActionResult Read()
    {

        //var listBooks = _repo.Book.Read();
        var listBooks = _repo.Book.GetBooksWithAuthorDetails();
        List<object> bookDetails = new List<object>(listBooks.Count());
        foreach(Book book in listBooks)
        {
            
            bookDetails.Add(new
             {
                bookName = book.BookName, 
                bookAuthorName = book.Author.AuthorName, 
                ownsAuthor = book.CreatedBy == book.Author.CreatedBy
            });
        }
        return Ok(bookDetails);
    }

    [HttpGet]
    [Route("{authorId}")]
    //return a specific book
    public IActionResult Read(Guid authorId)
    {
        var book = _repo.Book.GetBookWithAuthorDetails(authorId);
        var bookDetails = new 
        {
            bookName = book.BookName,
            datePublished = book.DatePublished
        };
        return Ok(bookDetails);
    }

    [HttpGet]
    [Route("{authorId}/{bookId}")]
    public IActionResult Read(Guid authorId, Guid bookId)
    {
        Book book = _repo.Book.GetBookWithAuthorDetails(authorId);
         var creatorName = _userManager.FindByIdAsync(book.CreatedBy.ToString()); 
        
        
        var bookDetails = new 
        {
            bookName = book.BookName, 
            datePublished = book.DatePublished,
            publisher = book.Publisher,
            copiesSold = book.copiesSold,
            creatorName = creatorName.Result.UserName
        };
       
        return Ok(bookDetails);
    }

}
}