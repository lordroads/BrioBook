using BrioBook.Users.DAL;
using BrioBook.Users.DAL.Models;

namespace Brio.Confirm.Services.Impl;

public class ConfirmRepository : IConfirmRepository
{
    private readonly BrioDbContext _context;

    public ConfirmRepository(BrioDbContext context)
    {
        _context = context;
    }


    public string Create(ConfirmId data)
    {
        var id = Guid.NewGuid();
        data.Id = id.ToString();
        _context.ConfirmIds.Add(data);

        return id.ToString();
    }

    public bool Delete(string key)
    {
        return _context.ConfirmIds.Remove(new ConfirmId { Id = key });
    }

    public ConfirmId Get(string key)
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
        var result = _context.ConfirmIds.Remove(data);

        if (result)
        {
            _context.ConfirmIds.Add(data);
        }

        return result;
    }
}