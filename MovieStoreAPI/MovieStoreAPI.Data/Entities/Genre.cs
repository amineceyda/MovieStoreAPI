
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStoreAPI.Data.Entities;

[Table("Genre", Schema = "dbo")]
public class Genre
{
    [Key]
    public int GenreId { get; set; }

    [Required]
    public string GenreName { get; set; }

}
