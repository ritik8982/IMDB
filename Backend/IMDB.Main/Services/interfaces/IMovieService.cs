using Assignment.Models.RequestModels;
using Assignment.Models.ResponseModel;
using System.Collections.Generic;

namespace Assignment.Services.interfaces
{
    public interface IMovieService
    {
        int Create(MovieRequest movieReqModel);
        void Update(int id,MovieRequest movieReqModel);
        void Delete(int id);
        MovieResponse Get(int id);
        List<MovieResponse> Get();
    }
}
