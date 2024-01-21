using Assignment.Models.RequestModels;
using Assignment.Models.ResponseModel;
using System.Collections.Generic;

namespace Assignment.Services.interfaces
{
    public interface IReviewService
    {
        int Create(ReviewRequest reviewModel);
        void Update(int id,ReviewRequest reviewModel);
        void Delete(int id);
        ReviewResponse Get(int id);
        List<ReviewResponse> Get();
    }
}
