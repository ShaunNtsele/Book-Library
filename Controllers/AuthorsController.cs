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
public class AuthorsController : ControllerBase
{


    private readonly IRepositoryWrapper _repo;
    private readonly UserManager<IdentityUser> _userManager;


    public AuthorsController(IRepositoryWrapper repo, UserManager<IdentityUser> userManager)
    {
        _repo = repo;
        _userManager = userManager;


    }


    [HttpPost]
    //create new author
    public IActionResult Create(Author author)
    {
        //get details of logged in user
        var currentUser = _userManager.FindByNameAsync(User.Identity.Name);
        var userId = currentUser.Result.Id;

        if (ModelState.IsValid)
        {

            _repo.Author.Create(author);
            author.CreatedBy = Guid.Parse(userId);
            _repo.Save();
            return Ok(author);
        }
        else
        {
            return StatusCode(StatusCodes.Status400BadRequest);
        }

    }

    [HttpGet]
    [Route("{authorId}")]
    //return a specific author
    public IActionResult Read(Guid authorId)
    {
        var author = _repo.Author.GetById(authorId);
        return Ok(author);
    }

    [HttpGet]
    [Route("")]
    //return list of all authors
    public IActionResult Read()
    {
        var listAuthors = _repo.Author.Read();
        return Ok(listAuthors);
    }

    [HttpDelete]
    [Route("{authorId}")]

    //delete specific author
    public IActionResult Delete(Guid authorId)
    {
        var author = _repo.Author.GetById(authorId);
        var currentUser = _userManager.FindByNameAsync(User.Identity.Name);
        var userId = currentUser.Result.Id;
        if (author == null)
            return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "Author does not exist" });
        else
        {
            if (author.CreatedBy == Guid.Parse(userId))
            {
                _repo.Author.Delete(author);
                _repo.Save();
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new Response { Status = "Error", Message = "You are not authorised to delete the user" });
            }

        }

    }

    [HttpPut]
    [Route("{authorId}")]
    public IActionResult Update(Author author)
    {
        var currentUser = _userManager.FindByNameAsync(User.Identity.Name);
        var userId = currentUser.Result.Id;
        if (author != null)
        {
            if (author.CreatedBy == Guid.Parse(userId))
            {
                _repo.Author.Update(author);
                _repo.Save();
                return Ok(author);
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new Response { Status = "Error", Message = "You are not authorised to update the user" });
            }

        }
        else
        {
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
}