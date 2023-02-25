using Brio.Database.DAL;
using Brio.Database.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Brio.Confirm.Services.Impl;

public class ConfirmRepository : IConfirmRepository
{
    private readonly BrioDbContext _context;

    public ConfirmRepository(BrioDbContext context)
    {
        _context = context;
    }


    public Guid Create(ConfirmId data)
    {
        var result = _context.ConfirmIds.Add(data);

        _context.SaveChanges();

        return result.Entity.Id;
    }

    public bool Delete(Guid key)
    {
        var entity = Get(key);

        var result = _context.ConfirmIds.Remove(entity);
        _context.SaveChanges();

        return result.State == EntityState.Deleted;
    }

    public ConfirmId Get(Guid key)
    {
        return _context.ConfirmIds.FirstOrDefault(confirm => confirm.Id == key);
    }

    public IList<ConfirmId> GetAll()
    {
        return _context.ConfirmIds.ToList();
    }

    public ConfirmId GetByValue(string value)
    {
        return _context
            .ConfirmIds
            .FirstOrDefault(confirm => confirm.UserId == Convert.ToInt32(value));
    }

    public bool Update(ConfirmId data)
    {
        var result = _context.ConfirmIds.Update(data);

        _context.SaveChanges();

        return result.State == EntityState.Modified;
    }
}