using Brio.Database.DAL.Models;

namespace Brio.UserManagement.Sevices;

public interface IUserService
{
    public int Add(User user);
    public void Remove(int id);
    public IList<User> GetUsers();
    public bool Update(User user);
}
