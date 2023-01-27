using BrioBook.Account.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrioBook.Account.Controllers;

[Authorize(Roles = "Admin")]
public class BookController : Controller
{
    public IActionResult Index()
    {
        List<Book> books = new List<Book>
        {
            new Book
            {
                Id= 1,
                Title = "Test Title",
                Author = "Test Author",
                Description = "Test Description",
                Genre = "Test Gener",
                Status = "Test"
            }
        };

        return View(books);
    }
}
