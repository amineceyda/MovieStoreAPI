using MovieStoreAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreAPI.Business.Services;

public interface IGenreService
{
    bool Add(Genre request);
    bool Update(Genre request);
    Genre GetById(int id);
    bool Delete(int id);
    IQueryable<Genre> List();

}