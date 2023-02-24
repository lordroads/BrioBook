namespace Brio.Authentication.Services;

public interface IConfirmServiceClient
{
    public string? CreateConfirmToUser(int userId);
}
