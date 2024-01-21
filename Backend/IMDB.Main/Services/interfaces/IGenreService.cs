using Assignment.Models.DBModels;
using Assignment.Models.RequestModels;
using Assignment.Models.ResponseModel;
using System.Collections.Generic;

namespace Assignment.Services.interfaces
{
    public interface IGenreService
    {
        int Create(GenreRequest GenreReqModel);
        void Update(int id,GenreRequest GenreReqModel);
        void Delete(int id);
        GenreResponse Get(int id);
        List<GenreResponse> Get();
        List<GenreResponse> GetByMovieId(int id);
    }
}
