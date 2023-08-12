using MovieStoreAPI.Business.Models;
using MovieStoreAPI.Data.Entities;

namespace MovieStoreAPI.Business.Services;

public interface IMovieService
{
    bool Add(Movie request);
    bool Update(Movie request);
    Movie GetById(int id);
    bool Delete(int id);
    MovieListResponse List(string term = "", bool paging = false, int currentPage = 0);
    List<int> GetGenreByMovieId(int movieId);

}
