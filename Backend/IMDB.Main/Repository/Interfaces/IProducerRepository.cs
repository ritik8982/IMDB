
using Assignment.Models.DBModels;
using System.Collections.Generic;

namespace Assignment.Repository.Interfaces
{
    public interface IProducerRepository
    {
        int Create(Producer producer);
        int Update(int id, Producer producer);
        int Delete(int id);
        Producer Get(int id);
        List<Producer> Get();
    }
}
