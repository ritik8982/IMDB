using Assignment.Models.DBModels;
using Assignment.Models.RequestModels;
using System.Collections.Generic;

namespace Assignment.Repository.Interfaces
{
    public interface IActorRepository
    {
        int Create(Actor actor);
        int Update(int id,Actor actor);
        int Delete(int id);
        Actor Get(int id);
        List<Actor> Get();
        List<Actor> GetByMovieId(int id);
    }
}
