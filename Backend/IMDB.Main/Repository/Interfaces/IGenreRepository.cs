using Assignment.Models.DBModels;
using Assignment.Models.RequestModels;
using System.Collections.Generic;

namespace Assignment.Repository.Interfaces
{
    public interface IGenreRepository
    {
        int Create(Genre genre);
        int Update(int id, Genre genre);
        int Delete(int id);
        Genre Get(int id);
        List<Genre> Get();
        public List<Genre> GetByMovieId(int id);
    }
}
