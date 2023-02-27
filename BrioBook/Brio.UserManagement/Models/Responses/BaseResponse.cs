namespace Brio.UserManagement.Models.Responses;

public abstract class BaseResponse
{
    public bool Succeeded { get; set; }
    public string Errors { get; set; }
}