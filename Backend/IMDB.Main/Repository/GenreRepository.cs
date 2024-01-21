using Assignment.Models.DBModels;
using Assignment.Repository.Interfaces;
using Assignment3;
using Assignment3.Repository;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace Assignment.Repository
{
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        public GenreRepository(IOptions<ConnectionString> connectionString) : base(connectionString.Value.IMDBDB)
        {
        }
        public int Create(Genre genre)
        {
            const string query = @"
            INSERT INTO Genres (Name)
            VALUES (@name);"; 
            return base.Create(query, new { name = genre.Name});
        }

        public int Delete(int id)
        {
            const string query = @"
            DELETE
            FROM Genres
            WHERE Id = @Id;";
            return base.Delete(query, new { Id = id });
        }

        public Genre Get(int id)
        {
            const string query = @"
            SELECT *
            FROM Genres
            WHERE Id = @Id;";
            return base.GetSingle(query, new { Id = id });
        }
        public List<Genre> GetByMovieId(int id)
        {
            const string query = @"
            SELECT g.Id
	            ,g.Name
            FROM Movies_Genres mg
            INNER JOIN Genres g ON mg.genreId = g.Id
            WHERE mg.movieId = @Id;";
            return base.Get(query, new { Id = id }).ToList();
        }

        public List<Genre> Get()
        {
            const string query = @"
            SELECT *
            FROM Genres;";
            return base.Get(query).ToList();
        }

        public int Update(int id, Genre genre)
        {
            const string query = @"
            UPDATE Genres
            SET Name = @Name
            WHERE Id = @Id;";
            return base.Update(query, new { Id = id, Name = genre.Name});
        }
    }
}
