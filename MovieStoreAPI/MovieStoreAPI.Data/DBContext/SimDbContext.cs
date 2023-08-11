using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieStoreAPI.Data.Entities;


namespace MovieStoreAPI.Data.DBContext;

public class SimDbContext : IdentityDbContext<User>
{
    public SimDbContext(DbContextOptions<SimDbContext> options) : base(options) { }

    public DbSet<Genre> Genre { get; set; }
    public DbSet<MovieGenre> MovieGenre { get; set; }
    public DbSet<Movie> Movie { get; set; }
}
