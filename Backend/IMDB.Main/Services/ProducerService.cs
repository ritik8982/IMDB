using Assignment.Models.RequestModels;
using Assignment.Models.ResponseModel;
using Assignment.Models.DBModels;
using Assignment.Repository;
using Assignment.Repository.Interfaces;
using Assignment.Services.interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Globalization;
using Assignment3.CustomException;

namespace Assignment.Services
{
    public class ProducerService : IProducerService
    {
        private readonly IProducerRepository _producerRepository;
        private readonly IMapper _mapper;
        public ProducerService(IProducerRepository producerRepository)
        {

            _producerRepository = producerRepository;
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProducerRequest, Producer>().ReverseMap();
                cfg.CreateMap<ProducerResponse, Producer>().ReverseMap();
                // Additional mapping configurations if needed
            });
            IMapper mapper = config.CreateMapper();
            _mapper = mapper;

        }
        public int Create(ProducerRequest producerRequest)
        {
            if (string.IsNullOrWhiteSpace(producerRequest.Name))
                throw new ArgumentException("producer name is empty");
            else if (string.IsNullOrWhiteSpace(producerRequest.Bio))
                throw new ArgumentException("producer Bio is empty");
            else if (string.IsNullOrWhiteSpace(producerRequest.Gender))
                throw new ArgumentException("producer Gender is empty");

            var producer = new Producer()
            {
                Name = producerRequest.Name,
                Bio = producerRequest.Bio,
                Gender = producerRequest.Gender,
            };
            DateTime parsedDob;
            if (DateTime.TryParseExact(producerRequest.DOB, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDob))
            {
                producer.DOB = parsedDob;
            }
            else
                throw new ArgumentException("producer dob is not valid, please provide valid date in dd/mm/yyyy format");
            return _producerRepository.Create(producer);
        }

        public void Delete(int id)
        {

            var noOfRowsAffected = _producerRepository.Delete(id);
            if (noOfRowsAffected <= 0)
                throw new EntityNotFoundException("there is not producer with provided id = " + id);
        }

        public ProducerResponse Get(int id)
        {
            var result = _mapper.Map<ProducerResponse>(_producerRepository.Get(id));
            if (result == null)
                throw new EntityNotFoundException("there is no producer with id = " + id);

            return result;
        }

        public List<ProducerResponse> Get()
        {
            return _mapper.Map<List<ProducerResponse>>(_producerRepository.Get());
        }

        public void Update(int id, ProducerRequest producerRequest)
        {
            if (string.IsNullOrWhiteSpace(producerRequest.Name))
                throw new ArgumentException("producer name is empty");
            else if (string.IsNullOrWhiteSpace(producerRequest.Bio))
                throw new ArgumentException("producer Bio is empty");
            else if (string.IsNullOrWhiteSpace(producerRequest.Gender))
                throw new ArgumentException("producer Gender is empty");

            var producer = new Producer()
            {
                Name = producerRequest.Name,
                Bio = producerRequest.Bio,
                Gender = producerRequest.Gender,
            };
            DateTime parsedDob;
            if (DateTime.TryParseExact(producerRequest.DOB, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDob))
            {
                producer.DOB = parsedDob;
            }
            else
                throw new ArgumentException("producer dob is not valid, please provide valid date in dd/mm/yyyy format");

            var noOfRowsAffected = _producerRepository.Update(id, producer);
            if (noOfRowsAffected <= 0)
                throw new EntityNotFoundException("there is no producer with  id = " + id);
        }
    }
}
