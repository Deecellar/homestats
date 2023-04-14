using System.ComponentModel.DataAnnotations;

namespace backend.Account;

public class ForgotPasswordRequest
{
    [Required] [EmailAddress] public string Email { get; set; }
}