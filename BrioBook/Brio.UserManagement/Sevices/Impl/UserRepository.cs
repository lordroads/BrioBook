using Brio.Database.DAL;
using Brio.Database.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Brio.UserManagement.Sevices.Impl;

public class UserRepository : IUserRepository
{
    private readonly BrioDbContext _context;

    public UserRepository(BrioDbContext context)
    {
        _context = context;
    }
    public int Create(User data)
    {
        var result = _context.Users.Add(data);

        _context.SaveChanges();

        return result.Entity.Id;
    }

    public bool Delete(int key)
    {
        var entity = _context.Users.FirstOrDefault(user => user.Id == key);

        var result = _context.Users.Remove(entity);

        _context.SaveChanges();

        return result.State == EntityState.Deleted;
    }

    public User Get(int key)
    {
        return _context.Users.FirstOrDefault(user => user.Id == key);
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
        _context.Update(data);

        var result = _context.SaveChanges();

        return result > 0;
    }
}