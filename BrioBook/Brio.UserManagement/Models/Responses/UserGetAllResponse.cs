using Brio.Database.DAL.Models;

namespace Brio.UserManagement.Models.Responses;

public class UserGetAllResponse : BaseResponse
{
    public IList<User> Users { get; set; }
}
