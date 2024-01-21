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
    public class ReviewService : IReviewService
    {
        private readonly IReviewsRepository _reviewsRepository;
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;
        public ReviewService(IReviewsRepository reviewRespository, IMovieService movieService)
        {
            _reviewsRepository = reviewRespository;
            _movieService = movieService;
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ReviewRequest, Review>().ReverseMap();
                cfg.CreateMap<ReviewResponse, Review>().ReverseMap();
                // Additional mapping configurations if needed
            });
            IMapper mapper = config.CreateMapper();
            _mapper = mapper;

        }
        public int Create(ReviewRequest reviewReqModel)
        {
            return _reviewsRepository.Create(_mapper.Map<Review>(reviewReqModel));
        }

        public void Delete(int id)
        {
            var noOfRowsAffected  =  _reviewsRepository.Delete(id);
            if (noOfRowsAffected <= 0)
                throw new EntityNotFoundException("there is no review with provided id = " + id);
        }

        public ReviewResponse Get(int id)
        {
            var result = _mapper.Map<ReviewResponse>(_reviewsRepository.Get(id));
            if (result == null)
                throw new EntityNotFoundException("there is no review by id = " + id);
           
            return result;
        }

        public List<ReviewResponse> Get()
        {
            return _mapper.Map<List<ReviewResponse>>(_reviewsRepository.Get());
        }

        public void Update(int id, ReviewRequest reviewReqModel)
        {
            var noOfRowsAffected = _reviewsRepository.Update(id, _mapper.Map<Review>(reviewReqModel));
            if (noOfRowsAffected <= 0)
                throw new EntityNotFoundException("there is not review with provided id = " + id);
        }
    }
}
