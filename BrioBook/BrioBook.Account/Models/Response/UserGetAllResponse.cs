using Brio.Database.DAL.Models;

namespace BrioBook.Client.Models.Response;

public class UserGetAllResponse : BaseResponse
{
    public IList<User> Users { get; set; }
}
