
using Assignment.Models.DBModels;
using Assignment.Repository.Interfaces;
using Assignment3;
using Assignment3.Repository;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Assignment.Repository
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        public MovieRepository(IOptions<ConnectionString> connectionString) : base(connectionString.Value.IMDBDB)
        {
        }
        public int Create(Movie movie,string actorIds,string genreIds)
        {
            return base.ExecuteStoredProcedure<int>("usp_movie_insert", 
               new { Id = movie.Id,Name = movie.Name, YearOfRelease = movie.YearOfRelease, Plot = movie.Plot, 
               CoverImage = movie.CoverImage,ProducerId = movie.ProducerId, ActorIds = actorIds , GenreIds  = genreIds});
        }

        public int Delete(int id)
        {
            const string query = @"
            DELETE
            FROM Movies
            WHERE Id = @Id;";
            return base.Delete(query, new { Id = id });
        }

        public Movie Get(int id)
        {
            const string query = @"
            SELECT *
            FROM Movies
            WHERE Id = @Id;";
            return base.GetSingle(query, new { Id = id });
        }

        public List<Movie> Get()
        {
            const string query = @"
            SELECT *
            FROM Movies;";
            return base.Get(query).ToList();
        }

        public int Update(int id, Movie movie, string actorIds, string genreIds)
        {
            object parameter = new
            {
                movieId = id,
                Name = movie.Name,
                YearOfRelease = movie.YearOfRelease,
                Plot = movie.Plot,
                CoverImage = movie.CoverImage,
                ProducerId = movie.ProducerId,
                actorIds = actorIds,
                genreIds = genreIds
            };
            return base.ExecuteStoredProcedure<int>("usp_movie_update", parameter);
        }
    }
}
