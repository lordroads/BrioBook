using Brio.Confirm.Models;
using BrioBook.Users.DAL.Models;

namespace Brio.Confirm.Services;

public interface IConfirmService
{
    public string Create(int userId);
    public bool SetConfirm(string confirmId);

    public IList<BigData> GetAll();
}