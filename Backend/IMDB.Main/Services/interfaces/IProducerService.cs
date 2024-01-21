using Assignment.Models.RequestModels;
using Assignment.Models.ResponseModel;
using System.Collections.Generic;

namespace Assignment.Services.interfaces
{
    public interface IProducerService
    {
        int Create(ProducerRequest producerReqModel);
        void Update(int id,ProducerRequest producerReqModel);
        void Delete(int id);
        ProducerResponse Get(int id);
        List<ProducerResponse> Get();
    }
}
