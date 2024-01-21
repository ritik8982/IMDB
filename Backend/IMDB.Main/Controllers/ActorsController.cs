using Assignment.Models.RequestModels;
using Assignment.Services;
using Assignment.Services.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using System;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly IActorService _actorService;
        public ActorsController(IActorService actorService) 
        {
            _actorService = actorService;
        }
        [HttpGet]
        public IActionResult Get()
        {
           return Ok(_actorService.Get());
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var actor = _actorService.Get(id);
                return Ok(actor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _actorService.Delete(id);
                return Ok("actor id = " + id + " has been deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] ActorRequest actorModel)
        {
            try
            {
                 var id = _actorService.Create(actorModel);
                 return Ok(new {message="actor successfully created",id=id,name=actorModel.Name});
                //we should send the details back so frontend can use it
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute]int id,[FromBody] ActorRequest actorModel)
        {
            try
            {
                _actorService.Update(id, actorModel);
                return Ok("actor successfully updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
    }
}
