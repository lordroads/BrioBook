namespace Brio.Confirm.Services;

public interface IConfirmService
{
    public Guid Create(int userId);
    public bool SetConfirm(Guid confirmId);
}