using BrioBook.Users.DAL;
using BrioBook.Users.DAL.Models;

namespace Brio.Confirm.Services.Impl;

public class UserRepository : IUserRepository
{
    private readonly BrioDbContext _context;

    public UserRepository(BrioDbContext context)
    {
        _context = context;
    }
    public int Create(User data)
    {
        int nextCount = _context.Users.Count + 1;

        _context.Users.Add(data);

        return nextCount;
    }

    public bool Delete(int key)
    {
        var result = _context.Users.FirstOrDefault(user => user.UserId.Equals(key));

        if (result is null)
        {
            return false;
        }

        _context.Users.Remove(result);

        return true;
    }

    public User Get(int key)
    {
        var user = _context.Users.FirstOrDefault(user => user.UserId.Equals(key));

        if (user is null)
        {
            return null;
        }

        return user;
    }

    public User GetByValue(string value)
    {
        return _context.Users.FirstOrDefault(user => user.Login.Equals(value));
    }

    public IList<User> GetAll()
    {
        return _context.Users.ToList();
    }

    public bool Update(User data)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserId == data.UserId);

        if (user is null)
        {
            return false;
        }

        _context.Users.Remove(user);

        _context.Users.Add(data);

        return true;
    }
}