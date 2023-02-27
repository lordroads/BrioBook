using Brio.Database.DAL.Models;
using BrioBook.Client.Models.Response;
using BrioBook.Client.Services;
using Microsoft.AspNetCore.Mvc;

namespace BrioBook.Client.Controllers
{
    public class UserController : Controller
    {
        private readonly IUsersClient _usersClient;

        public UserController(IUsersClient usersClient)
        {
            _usersClient = usersClient;
        }

        public ActionResult<UserGetAllResponse> Index()
        {
            return View(_usersClient.GetAll());
        }
    }
}
