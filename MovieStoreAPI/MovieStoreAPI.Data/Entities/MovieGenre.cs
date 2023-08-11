
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStoreAPI.Data.Entities;

[Table("MovieGenre", Schema = "dbo")]
public class MovieGenre
{
    [Key]
    public int MovieGenreId { get; set; }

    public int MovieId { get; set; }
    public Movie Movie { get; set; }

    public int GenreId { get; set; }
    public Genre Genre { get; set; }
}
