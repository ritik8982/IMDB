
using Assignment.Models.DBModels;
using System.Collections.Generic;

namespace Assignment.Repository.Interfaces
{
    public interface IReviewsRepository
    {
        int Create(Review review);
        int Update(int id, Review review);
        int Delete(int id);
        Review Get(int id);
        List<Review> Get();
    }
}
