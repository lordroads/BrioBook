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

    public Guid Create(int userId)
    {
        Guid confirmId = _confirmRepository.Create(new ConfirmId
        {
            UserId = userId
        });

        return confirmId;
    }

    public bool SetConfirm(Guid confirmId)
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