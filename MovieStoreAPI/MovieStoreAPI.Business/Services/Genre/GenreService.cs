using MovieStoreAPI.Data.DBContext;
using MovieStoreAPI.Data.Entities;

namespace MovieStoreAPI.Business.Services;

public class GenreService : IGenreService
{
    private readonly SimDbContext _dbContext;
    public GenreService(SimDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool Add(Genre request)
    {
        try
        {
            _dbContext.Genre.Add(request); 
            _dbContext.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool Update(Genre request)
    {
        try
        {
            _dbContext.Genre.Update(request);
            _dbContext.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public Genre GetById(int id)
    {
        var genre = _dbContext.Genre.Find(id);
        return genre;
    }

    public bool Delete(int id)
    {
        try
        {
            var genre = this.GetById(id);
            if (genre is null)
                return false;
            _dbContext.Genre.Remove(genre);
            _dbContext.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public IQueryable<Genre> List()
    {
        var genreList = _dbContext.Genre.AsQueryable();
        return genreList;
    }


}
