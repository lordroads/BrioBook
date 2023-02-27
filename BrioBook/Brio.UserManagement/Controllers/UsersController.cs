using Brio.UserManagement.Models.Responses;
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
    public ActionResult<UserGetAllResponse> GetAll()
    {
        try
        {
            return Ok(new UserGetAllResponse
            {
                Succeeded = true,
                Users = _userService.GetUsers()
            });
        }
        catch (Exception ex)
        {
            return Ok(new UserGetAllResponse 
            { 
                Errors = ex.Message,
                Succeeded = false
            });

        }
    }

}
