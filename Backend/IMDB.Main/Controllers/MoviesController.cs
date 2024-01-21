using Assignment.Models.RequestModels;
using Assignment.Services.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Assignment.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_movieService.Get());
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
               var movie =  _movieService.Get(id);
                return Ok(movie);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _movieService.Delete(id);
                return Ok("movie id = " + id + " has been deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] MovieRequest movieReqModel)
        {
            try
            {
                var id = _movieService.Create(movieReqModel);
                return Ok("Movie successfully added and the id of movie is " + id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] MovieRequest movieReqModel)
        {
            try
            {
                _movieService.Update(id, movieReqModel);
                return Ok("Movie successfully updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
