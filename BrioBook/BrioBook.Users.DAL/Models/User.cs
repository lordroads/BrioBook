using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Brio.Database.DAL.Models;

public class User
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required, StringLength(255)]
    public string Login { get; set; }

    [Required, StringLength(100)]
    public string PasswordHash { get; set; }

    [Required, StringLength(100)]
    public string PasswordSalt { get; set; }

    public bool IsAdmin { get; set; }

    public bool ConfirmEmail { get; set; }
}