using BrioBook.Client.Models.Response;

namespace BrioBook.Client.Services;

public interface IConfirmClient
{
    public SetConfirmResponse SetConfirm(Guid id);
}
