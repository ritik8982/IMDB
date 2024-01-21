using Assignment.Models.RequestModels;
using Assignment.Models.ResponseModel;
using Assignment.Models.DBModels;
using Assignment.Repository.Interfaces;
using Assignment.Services.interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Assignment.Repository;
using Assignment3.CustomException;

namespace Assignment.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _repository;
        private readonly IActorService _actorService;
        private readonly IGenreService _genreService;

        private readonly IMapper _mapper;
        public MovieService(IMovieRepository repository, IActorService actorService, IGenreService genreService)
        {
            _repository = repository;
            _actorService = actorService;
            _genreService = genreService;

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MovieRequest, Movie>().ReverseMap();
                cfg.CreateMap<MovieResponse, Movie>().ReverseMap();
                cfg.CreateMap<ActorResponse,Actor>().ReverseMap();
                cfg.CreateMap<GenreResponse,Genre>().ReverseMap();
            });
            IMapper mapper = config.CreateMapper();
            _mapper = mapper;

        }
        public int Create(MovieRequest movieReq)
        {
            if (string.IsNullOrWhiteSpace(movieReq.Name))
                throw new ArgumentException("movie's name is empty");
            if (string.IsNullOrWhiteSpace(movieReq.Plot))
                throw new ArgumentException("movie's plot is empty");
            if (string.IsNullOrWhiteSpace(movieReq.CoverImage))
                throw new ArgumentException("Movie's cover image is empty");
            if (movieReq.YearOfRelease <= 1900)
                throw new ArgumentException("Movie's year of release is not valid");
            if(movieReq.ProducerId < 0)
                throw new ArgumentException("Movie's producer id is not valid");

            return _repository.Create(_mapper.Map<Movie>(movieReq), movieReq.ActorIds, movieReq.GenreIds); 
        }

        public void Delete(int id)
        {
            var noOfRowsAffected = _repository.Delete(id);
            if (noOfRowsAffected <= 0)
                throw new EntityNotFoundException("there is no movie with id = " + id);
        }

        public MovieResponse Get(int id)
        {
            var movie = _mapper.Map<MovieResponse>(_repository.Get(id));
            if (movie == null)
                throw new EntityNotFoundException("there is no movie with id = "+id);

            movie.Actors = _actorService.GetByMovieId(movie.Id);
            movie.Genres = _genreService.GetByMovieId(movie.Id);

            return movie;
        }

        public List<MovieResponse> Get()
        {
            var movies = _mapper.Map<List<MovieResponse>>(_repository.Get());
            for (int i = 0; i < movies.Count; i++)
            {
                movies[i].Actors = _mapper.Map<List<ActorResponse>>(_actorService.GetByMovieId(movies[i].Id));
                movies[i].Genres = _mapper.Map<List<GenreResponse>>(_genreService.GetByMovieId(movies[i].Id));
            }
                return movies;
        }

        public void Update(int id, MovieRequest movieReq)
        {
            if (string.IsNullOrWhiteSpace(movieReq.Name))
                throw new ArgumentException("movie's name is empty");
            if (string.IsNullOrWhiteSpace(movieReq.Plot))
                throw new ArgumentException("movie's plot is empty");
            if (string.IsNullOrWhiteSpace(movieReq.CoverImage))
                throw new ArgumentException("Movie's cover image is empty");
            if (movieReq.YearOfRelease <= 1900)
                throw new ArgumentException("Movie's year of release is not valid");
            if (movieReq.ProducerId < 0)
                throw new ArgumentException("Movie's producer id is not valid");

            var noOfRowsAffected = _repository.Update(id, _mapper.Map<Movie>(movieReq), movieReq.ActorIds, movieReq.GenreIds);

            if (noOfRowsAffected <= 0)
                throw new EntityNotFoundException("there is no movie with id = " + id);
        }
    }
}
