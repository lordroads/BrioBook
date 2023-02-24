using Brio.Confirm.Models;
using BrioBook.Users.DAL.Models;

namespace Brio.Confirm.Services.Impl;

public class ConfirmService : IConfirmService
{
    private readonly IConfirmRepository _confirmRepository;
    private readonly IUserRepository _userRepository;

    public ConfirmService(IConfirmRepository confirmRepository, IUserRepository userRepository)
    {
        _confirmRepository = confirmRepository;
        _userRepository = userRepository;
    }

    public string Create(int userId)
    {
        string confirmId = _confirmRepository.Create(new ConfirmId
        {
            UserId = userId
        });

        return confirmId;
    }

    public IList<BigData> GetAll()
    {
        var confirmIds = _confirmRepository.GetAll();
        var users = _userRepository.GetAll();

        return confirmIds.Select(item => new BigData 
        { 
            Id = item.Id,
            UserId = item.UserId,
            User = users[item.UserId]
        }).ToList();
    }

    public bool SetConfirm(string confirmId)
    {
        var data = _confirmRepository.Get(confirmId);

        if (data is null)
        {
            return false;
        }

        var user = _userRepository.Get(data.UserId);


        if (user is null)
        {
            return false;
        }

        user.ConfirmEmail = true;

        return _userRepository.Update(user);
    }
}