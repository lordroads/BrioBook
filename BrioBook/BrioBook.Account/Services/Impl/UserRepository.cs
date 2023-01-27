using BrioBook.Account.Models;
using BrioBook.Users.DAL;

namespace BrioBook.Account.Services.Impl;

public class UserRepository : IUserRepository
{
    private readonly UsersDbContext _context;

    public UserRepository(UsersDbContext context)
    {
        _context = context;
    }

    public int Add(User entity)
    {
        int nextCount = _context.Users.Count + 1;

        _context.Users.Add(new Users.DAL.Models.User
        {
            UserId = entity.UserId = nextCount,
            Login = entity.Login,
            PasswordHash = entity.PasswordHash,
            PasswordSalt = entity.PasswordSalt
        });

        return nextCount;
    }

    public bool Delete(int id)
    {
        var result = _context.Users.FirstOrDefault(user => user.UserId.Equals(id));

        if (result is null)
        {
            return false;
        }

        _context.Users.Remove(result);

        return true;
    }

    public ICollection<User> GetAll()
    {
        return _context.Users.Select(user => new User
        {
            UserId = user.UserId,
            Login = user.Login,
            PasswordHash = user.PasswordHash,
            PasswordSalt = user.PasswordSalt 
        }).ToList();
    }

    public User GetById(int id)
    {
        var user = _context.Users.FirstOrDefault(user => user.UserId.Equals(id));

        if (user is null)
        {
            return null;
        }

        return new User()
        {
            UserId = user.UserId,
            Login = user.Login,
            PasswordHash = user.PasswordHash,
            PasswordSalt = user.PasswordSalt
        };
    }

    public User GetByValue(string value)
    {
        var user = _context.Users.FirstOrDefault(user => user.Login.Equals(value));

        if (user is null)
        {
            return null;
        }

        return new User()
        {
            UserId = user.UserId,
            Login = user.Login,
            PasswordHash = user.PasswordHash,
            PasswordSalt = user.PasswordSalt
        };
    }

    public bool Update(User entity)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserId == entity.UserId);
        
        if (user is null)
        {
            return false;
        }

        _context.Users.Remove(user);

        _context.Users.Add(new Users.DAL.Models.User
        {
            UserId = entity.UserId,
            Login = entity.Login,
            PasswordHash = entity.PasswordHash,
            PasswordSalt = entity.PasswordSalt
        });

        return true;
    }
}
