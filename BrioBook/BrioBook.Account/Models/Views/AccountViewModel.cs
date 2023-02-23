using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BrioBook.Account.Models.Views;

public class AccountViewModel : IValidatableObject
{
    [Required(ErrorMessage = "���������� ������")]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"^[A-Za-z0-9%+-]+@[A-Za-z0-9-]+.+.[A-Za-z]{2,4}$", ErrorMessage = "�� ��������� ������ ����� �����")]
    public string Login { get; set; }

    [Required(ErrorMessage = "���������� ������")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "���������� ������")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "������ �� ���������")]
    public string ConfirmPassword { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {

        if (string.IsNullOrWhiteSpace(Login))
        {
            yield return new ValidationResult("������� ����������� �����!", new List<string>() { "Login" });
        }
        if (string.IsNullOrWhiteSpace(Password))
        {
            yield return new ValidationResult("������� ������!", new List<string>() { "Password" });
        }
        if (string.IsNullOrWhiteSpace(ConfirmPassword))
        {
            yield return new ValidationResult("������� ������������� ������!", new List<string>() { "ConfirmPassword" });
        }

        yield break;
    }
}