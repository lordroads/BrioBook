using Brio.Database.DAL.Models;
using Brio.UserManagement.Sevices;
using Microsoft.AspNetCore.Mvc;

namespace Brio.UserManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }


    [HttpGet("get-all")]
    public ActionResult<IList<User>> GetAll()
    {
        return Ok(_userService.GetUsers());
    }

}
