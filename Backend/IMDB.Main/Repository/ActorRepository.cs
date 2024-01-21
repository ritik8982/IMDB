using Assignment.Models.DBModels;
using Assignment.Models.RequestModels;
using Assignment.Repository.Interfaces;
using Assignment3;
using Assignment3.Repository;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace Assignment.Repository
{
    public class ActorRepository : BaseRepository<Actor>, IActorRepository
    {

        public ActorRepository(IOptions<ConnectionString> connectionString) : base(connectionString.Value.IMDBDB)
        {
        }
        public int Create(Actor actor)
        {
            const string query = @"
            INSERT INTO Actors (
	            Name
	            ,Bio
	            ,DOB
	            ,Gender
	            )
            VALUES (
	            @name
	            ,@bio
	            ,@dob
	            ,@gender
	            );";
            return base.Create(query, new { name = actor.Name, bio = actor.Bio, dob = actor.DOB.ToString("MM/dd/yyyy"), gender = actor.Gender });
        }

        public int Delete(int id)
        {
            const string query = @"
            DELETE
            FROM Actors
            WHERE Id = @Id;";
            return base.Delete(query, new { Id = id });
        }

        public Actor Get(int id)
        {
            const string query = @"
            SELECT *
            FROM Actors
            WHERE Id = @Id;";
            return base.GetSingle(query, new { Id = id });
        }

        public List<Actor> Get()
        {
            const string query = @"
            SELECT *
            FROM Actors;";
            return base.Get(query).ToList();
        }

        public List<Actor> GetByMovieId(int id)
        {
            const string query = @"
            SELECT a.Id
	        ,a.Name
	        ,a.Bio
	        ,a.DOB
	        ,a.Gender
            FROM Movies_Actors ma
            INNER JOIN Actors a ON ma.actorId = a.Id
            WHERE ma.movieId = @Id;";
            return base.Get(query, new { Id = id }).ToList();
        }

        public int Update(int id, Actor actor)
        {
            const string query = @"
            UPDATE Actors
            SET Name = @Name
	            ,Bio = @Bio
	            ,DOB = @DOB
	            ,Gender = @gender
            WHERE Id = @Id;";
            return base.Update(query, new { Id = id, Name = actor.Name, Bio = actor.Bio, DOB = actor.DOB.ToString("MM/dd/yyyy"), Gender = actor.Gender });
        }
    }
}
