using Brio.Database.DAL.Models;

namespace Brio.UserManagement.Sevices.Impl;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public int Add(User user)
    {
        throw new NotImplementedException();
    }

    public IList<User> GetUsers()
    {
        return _userRepository.GetAll();
    }

    public void Remove(int id)
    {
        throw new NotImplementedException();
    }

    public bool Update(User user)
    {
        throw new NotImplementedException();
    }
}
