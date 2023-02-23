using BrioBook.Users.DAL.Models;

namespace BrioBook.Users.DAL;

public class BrioDbContext
{
    //TODO: ���� �� ������������ DB

    public List<User> Users { get; set; }
    public List<ConfirmId> ConfirmIds { get; set; }
    public BrioDbContext()
    {
        Users = new List<User>();
        ConfirmIds = new List<ConfirmId>();
    }


}