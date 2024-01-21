using Assignment.Models.DBModels;
using Assignment.Models.RequestModels;
using Assignment.Models.ResponseModel;
using System.Collections.Generic;

namespace Assignment.Services.interfaces
{
    public interface IActorService
    {
        int Create(ActorRequest actorModel);
        void Update(int id,ActorRequest actorModel);
        void Delete(int id);
        ActorResponse Get(int id);
        List<ActorResponse> Get();
        List<ActorResponse> GetByMovieId(int id);
    }
}
