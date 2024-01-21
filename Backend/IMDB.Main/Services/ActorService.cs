using Assignment.Models.DBModels;
using Assignment.Models.RequestModels;
using Assignment.Models.ResponseModel;
using Assignment.Repository.Interfaces;
using Assignment.Services.interfaces;
using Assignment3.CustomException;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;

namespace Assignment.Services
{
    public class ActorService : IActorService
    {
        private readonly IActorRepository _actorRepository;
        private readonly IMapper _mapper;
        public ActorService(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ActorRequest, Actor>().ReverseMap();
                cfg.CreateMap<ActorResponse, Actor>().ReverseMap();
                // Additional mapping configurations if needed
            });
            IMapper mapper = config.CreateMapper();
            _mapper = mapper;
        }

        public int Create(ActorRequest actorReqModel)
        {
            if (string.IsNullOrWhiteSpace(actorReqModel.Name))
                throw new ArgumentException("actor name is empty");
            else if (string.IsNullOrWhiteSpace(actorReqModel.Bio))
                throw new ArgumentException("actor Bio is empty");
            else if (string.IsNullOrWhiteSpace(actorReqModel.Gender))
                throw new ArgumentException("actor Gender is empty");

            var actor = new Actor()
            {
                Name = actorReqModel.Name,
                Bio = actorReqModel.Bio,
                Gender = actorReqModel.Gender,
            };
            DateTime parsedDob;
            if (DateTime.TryParseExact(actorReqModel.DOB, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDob))
            {
                actor.DOB = parsedDob;
            }
            else
                throw new ArgumentException("actor dob is not valid, please provide valid date in dd/mm/yyyy format");
            return _actorRepository.Create(actor);
        }

        public void Delete(int id)
        {
            var noOfRowsAffected = _actorRepository.Delete(id);
            if (noOfRowsAffected <= 0)
                throw new EntityNotFoundException("there is not actor with provided id = " + id);
        }

        public ActorResponse Get(int id)
        {
            var result = _mapper.Map<ActorResponse>(_actorRepository.Get(id));
            if (result == null)
                throw new EntityNotFoundException("there is no actor with id = " + id);
            return result;
        }

        public List<ActorResponse> Get()
        {
            return _mapper.Map<List<ActorResponse>>(_actorRepository.Get());
        }

        public List<ActorResponse> GetByMovieId(int id)
        {
            return _mapper.Map<List<ActorResponse>>(_actorRepository.GetByMovieId(id));
        }

        public void Update(int id, ActorRequest actorReqModel)
        {
            Console.WriteLine(actorReqModel.ToString());
            if (string.IsNullOrWhiteSpace(actorReqModel.Name))
                throw new ArgumentException("actor name is empty");
            else if (string.IsNullOrWhiteSpace(actorReqModel.Bio))
                throw new ArgumentException("actor Bio is empty");
            else if (string.IsNullOrWhiteSpace(actorReqModel.Gender))
                throw new ArgumentException("actor Gender is empty");

            var actor = new Actor()
            {
                Name = actorReqModel.Name,
                Bio = actorReqModel.Bio,
                Gender = actorReqModel.Gender,
            };
            DateTime parsedDob;
            if (DateTime.TryParseExact(actorReqModel.DOB, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDob))
            {
                actor.DOB = parsedDob;
            }
            else
                throw new ArgumentException("actor dob is not valid, please provide valid date in dd/mm/yyyy format");

            var noOfRowsAffected = _actorRepository.Update(id, actor);
            if (noOfRowsAffected <= 0)
                throw new EntityNotFoundException("there is not actor with provided id = " + id);
        }
    }
}
