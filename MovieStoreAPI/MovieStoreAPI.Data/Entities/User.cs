using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStoreAPI.Data.Entities;

[Table("User", Schema = "dbo")]
public class User : IdentityUser
{
    public string Name { get; set; }

}
