
using Assignment.Models.DBModels;
using Assignment.Repository.Interfaces;
using Assignment3;
using Assignment3.Repository;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace Assignment.Repository
{
    public class ReviewRepository : BaseRepository<Review>, IReviewsRepository
    {
        public ReviewRepository(IOptions<ConnectionString> connectionString) : base(connectionString.Value.IMDBDB)
        {
        }
        public int Create(Review review)
        {
            const string query = @"
            INSERT INTO Reviews (
	            Message
	            ,MovieId
	            )
            VALUES (
	            @Message
	            ,@movieId
	            );";
            return base.Create(query, new { Message = review.Message, MovieId = review.MovieId});
        }

        public int Delete(int id)
        {
            const string query = @"
            DELETE
            FROM Reviews
            WHERE Id = @Id;";
            return base.Delete(query, new { Id = id });
        }

        public Review Get(int id)
        {
            const string query = @"
            SELECT *
            FROM Reviews
            WHERE Id = @Id;";
            return base.GetSingle(query, new { Id = id });
        }

        public List<Review> Get()
        {
            const string query = @"
            SELECT *
            FROM Reviews;";
            return base.Get(query).ToList();
        }

        public int Update(int id, Review review)
        {
            const string query = @"
            UPDATE Reviews
            SET Message = @Message
	            ,MovieId = @movieId
            WHERE Id = @Id;";
            return base.Update(query, new { Id = id, Message = review.Message, movieId = review.MovieId });
        }
    }
}
