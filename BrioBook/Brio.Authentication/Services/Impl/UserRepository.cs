using Brio.Database.DAL;
using Brio.Database.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Brio.Authentication.Services.Impl;

public class UserRepository : IUserRepository
{
    private readonly BrioDbContext _context;

    public UserRepository(BrioDbContext context)
    {
        _context = context;
    }
    public int Create(User data)
    {
        var entity = _context.Users.Add(data);

        _context.SaveChanges();

        return entity.Entity.Id;
    }

    public bool Delete(int key)
    {
        var result = _context.Users.FirstOrDefault(user => user.Id == key);

        if (result is null)
        {
            return false;
        }

        _context.Users.Remove(result);
        _context.SaveChanges();

        return true;
    }

    public User Get(int key)
    {
        return _context.Users.FirstOrDefault(user => user.Id == key);
    }

    public User GetByValue(string value)
    {
        return _context.Users.FirstOrDefault(user => user.Login == value);
    }

    public IList<User> GetAll()
    {
        return _context.Users.ToList();
    }

    public bool Update(User data)
    {
        var result = _context.Users.Update(data);

        _context.SaveChanges();

        return result.State == EntityState.Modified;
    }
}
