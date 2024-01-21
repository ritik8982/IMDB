
using Assignment.Models.DBModels;
using Assignment.Repository.Interfaces;
using Assignment3;
using Assignment3.Repository;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace Assignment.Repository
{
    public class ProducerRepository : BaseRepository<Producer>, IProducerRepository
    {
        public ProducerRepository(IOptions<ConnectionString> connectionString) : base(connectionString.Value.IMDBDB)
        {
        }
        public int Create(Producer producer)
        {
            const string query = @"
            INSERT INTO Producers (
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
            return base.Create(query, new { name = producer.Name, bio = producer.Bio, dob = producer.DOB.ToString("MM/dd/yyyy"), gender = producer.Gender });
        }

        public int Delete(int id)
        {
            const string query = @"
            DELETE
            FROM Producers
            WHERE Id = @Id;";
            return base.Delete(query, new { Id = id });
        }

        public Producer Get(int id)
        {
            const string query = @"
            SELECT *
            FROM Producers
            WHERE Id = @Id;";
            return base.GetSingle(query, new { Id = id });
        }

        public List<Producer> Get()
        {
            const string query = @"
            SELECT *
            FROM Producers;";
            return base.Get(query).ToList();
        }

        public int Update(int id, Producer producer)
        {
            const string query = @"
            UPDATE Producers
            SET Name = @Name
	            ,Bio = @Bio
	            ,DOB = @DOB
	            ,Gender = @gender
            WHERE Id = @Id;";
            return base.Update(query, new { Id = id, Name = producer.Name, Bio = producer.Bio, DOB = producer.DOB.ToString("MM/dd/yyyy"), Gender = producer.Gender });
        }
    }
}
