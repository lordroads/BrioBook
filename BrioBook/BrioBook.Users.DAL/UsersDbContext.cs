using BrioBook.Users.DAL.Models;

namespace BrioBook.Users.DAL;

public class UsersDbContext
{
    //TODO: Пока не использовать DB

    public List<User> Users { get; set; }
    public UsersDbContext()
    {
        Users = new List<User>();
    }


}