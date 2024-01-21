
using Assignment.Models.DBModels;
using System.Collections.Generic;

namespace Assignment.Repository.Interfaces
{
    public interface IMovieRepository
    {
        int Create(Movie movie,string ActorIds,string MovieIds);
        int Update(int id, Movie movie, string actorIds, string genreIds);
        int Delete(int id);
        Movie Get(int id);
        List<Movie> Get();
    }
}
