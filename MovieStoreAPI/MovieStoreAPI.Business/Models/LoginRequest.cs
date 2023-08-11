

using System.ComponentModel.DataAnnotations;

namespace MovieStoreAPI.Business.Models;

public class LoginRequest
{
    [Required]
    public string? Username { get; set; }

    [Required]
    public string? Password { get; set; }
}
