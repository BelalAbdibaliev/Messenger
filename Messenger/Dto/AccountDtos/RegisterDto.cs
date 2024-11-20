using System.ComponentModel.DataAnnotations;

namespace Messenger.Dto;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
    [Required]
    [Compare(nameof(Password))]
    public string ConfirmedPassword { get; set; } = string.Empty;
    [Required]
    public string UserName { get; set; } = string.Empty;
    [Required]
    public string FirstName { get; set; } = string.Empty; 
    [Required]
    public string LastName { get; set; } = string.Empty;
}