using Assignment.Models.RequestModels;
using Assignment.Models.ResponseModel;
using Assignment.Models.DBModels;
using Assignment.Repository.Interfaces;
using Assignment.Services.interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using Assignment.Repository;
using System.Numerics;
using Assignment3.CustomException;

namespace Assignment.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;
        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GenreRequest, Genre>().ReverseMap();
                cfg.CreateMap<GenreResponse, Genre>().ReverseMap();
            });
            IMapper mapper = config.CreateMapper();
            _mapper = mapper;
        }
        public int Create(GenreRequest genreReqModel)
        {
            if (string.IsNullOrWhiteSpace(genreReqModel.Name))
                throw new ArgumentException("genre name is empty");
            return _genreRepository.Create(_mapper.Map<Genre>(genreReqModel));

        }

        public void Delete(int id)
        {
            var noOfRowsAffected = _genreRepository.Delete(id);
            if (noOfRowsAffected <= 0)
                throw new EntityNotFoundException("there is no genre with provided id = " + id);
        }

        public GenreResponse Get(int id)
        {
            var result = _mapper.Map<GenreResponse>(_genreRepository.Get(id));
            if (result == null)
                throw new EntityNotFoundException("there is no genre with id = " + id); 
            return result;
        }

        public List<GenreResponse> Get()
        {
            return _mapper.Map<List<GenreResponse>>(_genreRepository.Get());
        }

        public List<GenreResponse> GetByMovieId(int id)
        {
            return _mapper.Map<List<GenreResponse>>(_genreRepository.GetByMovieId(id));  
        }

        public void Update(int id,GenreRequest genreReqModel)
        {
            if (string.IsNullOrWhiteSpace(genreReqModel.Name))
                throw new ArgumentException("genre name is empty");

            var noOfRowsAffected = _genreRepository.Update(id, _mapper.Map<Genre>(genreReqModel));
            if (noOfRowsAffected <= 0)
                throw new EntityNotFoundException("there is no genre with provided id = " + id);

        }
    }
}
