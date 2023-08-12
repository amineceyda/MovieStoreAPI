using MovieStoreAPI.Business.Models;
using MovieStoreAPI.Data.DBContext;
using MovieStoreAPI.Data.Entities;


namespace MovieStoreAPI.Business.Services;

public class MovieService : IMovieService
{
    private readonly SimDbContext _dbContext;
    public MovieService(SimDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool Add(Movie request)
    {
        try
        {
            _dbContext.Movie.Add(request);
            _dbContext.SaveChanges();
            foreach(int genreId in request.GenreIds)
            {
                var movieGenre = new MovieGenre
                {
                    MovieId = request.MovieId,
                    GenreId = genreId
                };
                _dbContext.MovieGenre.Add(movieGenre);
            }
            _dbContext.SaveChanges();
            return true;

        }
        catch (Exception ex)
        {
            return false;
        
        }
    }



    public bool Delete(int id)
    {
        try
        {
            var movie = this.GetById(id);
            if (movie is null)
                return false;
            var movieGenres = _dbContext.MovieGenre.Where(m => m.MovieId == movie.MovieId);
            foreach(var movieGenre in movieGenres)
            {
                _dbContext.MovieGenre.Remove(movieGenre);
            }
            _dbContext.Movie.Remove(movie); 
            _dbContext.SaveChanges();
            return true;
      

        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public Movie GetById(int id)
    {
        var movie = _dbContext.Movie.Find(id); 
        return movie;
    }

    public List<int> GetGenreByMovieId(int movieId)
    {
        var genreIds = _dbContext.MovieGenre.Where(m => m.MovieId == movieId).Select(g => g.GenreId).ToList();
        return genreIds;
    }

    public MovieListResponse List(string term = "", bool paging = false, int currentPage = 0)
    {
        var movieListResponse = new MovieListResponse();

        var movielist = _dbContext.Movie.ToList();


        if (!string.IsNullOrEmpty(term))
        {
            term = term.ToLower();
            movielist = movielist.Where(m => m.Title.ToLower().StartsWith(term)).ToList();
        }

        if (paging)
        {
            // here we will apply paging
            int pageSize = 5;
            int count = movielist.Count;
            int TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            movielist = movielist.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            movieListResponse.PageSize = pageSize;
            movieListResponse.CurrentPage = currentPage;
            movieListResponse.TotalPages = TotalPages;
        }

        foreach (var movie in movielist)
        {
            var genres = (from genre in _dbContext.Genre
                          join mg in _dbContext.MovieGenre
                          on genre.GenreId equals mg.GenreId
                          where mg.MovieId == movie.MovieId
                          select genre.GenreName
                          ).ToList();
            var genreNames = string.Join(',', genres);
            movie.GenreNames = genreNames;
        }
        movieListResponse.MovieList = movielist.AsQueryable();
        return movieListResponse;
    }

    public bool Update(Movie request)
    {
        try
        {
            // these genreIds are not selected by users and still present is movieGenre table corresponding to
            // this movieId. So these ids should be removed.
            var genresToDeleted = _dbContext.MovieGenre.Where(a => a.MovieId == request.MovieId && !request.GenreIds.Contains(a.GenreId)).ToList();
            foreach (var mg in genresToDeleted)
            {
                _dbContext.MovieGenre.Remove(mg);
            }
            foreach (int genreId in request.GenreIds)
            {
                var movieGenre = _dbContext.MovieGenre.FirstOrDefault(a => a.MovieId == request.MovieId && a.GenreId == genreId);
                if (movieGenre == null)
                {
                    movieGenre = new MovieGenre { GenreId = genreId, MovieId = request.MovieId };
                    _dbContext.MovieGenre.Add(movieGenre);
                }
            }

           _dbContext.Movie.Update(request);
            _dbContext.SaveChanges();
            return true;

        }
        catch (Exception ex)
        {

            return false;
        }
    }

}
