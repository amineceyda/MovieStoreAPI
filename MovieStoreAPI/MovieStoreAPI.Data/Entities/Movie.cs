using MovieStoreAPI.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStoreAPI.Data.Entities;

[Table("Movie", Schema = "dbo")]
public class Movie
{

    [Key]
    public int MovieId { get; set; }
    public string Title { get; set; }
    public DateTime ReleaseTime { get; set; }
    public string MovieImage { get; set; }
    public string Cast { get; set; }
    public string Director { get; set; }

    public List<int>? GenreIds { get; set; }
    public string? GenreNames { get; set; }
    public virtual ICollection<MovieGenre> MovieGenres { get; set; }
}
