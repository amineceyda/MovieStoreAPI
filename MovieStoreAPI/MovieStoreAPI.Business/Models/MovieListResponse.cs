using MovieStoreAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreAPI.Business.Models;

public class MovieListResponse
{//also for pagination
    public IQueryable<Movie> MovieList { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public string? Term { get; set; }
}
