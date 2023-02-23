﻿using BrioBook.Users.DAL;
using BrioBook.Users.DAL.Models;

namespace Brio.Authentication.Services.Impl;

public class ConfirmIdsRepository : IConfirmIdsRepository
{
    private readonly BrioDbContext _context;

    public ConfirmIdsRepository(BrioDbContext context)
    {
        _context = context;
    }


    public Guid Create(ConfirmId data)
    {
        var id = Guid.NewGuid();
        data.Id = id;
        _context.ConfirmIds.Add(data);

        return id;
    }

    public bool Delete(Guid key)
    {
        return _context.ConfirmIds.Remove(new ConfirmId { Id = key });
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
        var result = _context.ConfirmIds.Remove(data);

        if (result)
        {
            _context.ConfirmIds.Add(data);
        }

        return result;
    }
}
