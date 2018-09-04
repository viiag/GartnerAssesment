using System;
using System.Web.Http;
using Mars_Rover_Webservices.Models;

namespace Mars_Rover_Webservices.Controllers
{
    // Nominally all data would be persisted inside a database. Instead we're using
    // a static in memory context to persist values across calls since the excercise
    // deemed this to be sufficient. -- MWK 9/3/2018
    [RoutePrefix("api/rovers")]
    public class RoverController : ApiController
    {
        [Route("{roverId:int}/positions")]
        [HttpGet]
        public IHttpActionResult GetRoverPosition([FromUri] int? RoverId)
        {
            // We don't need to do any validation here, it'll either pull from a query string
            // parameter that is provided, or it will fall back upon using the part of the resource URI
            // that holds the rover ID
            if (RoverContext.Rovers.ContainsKey(RoverId.Value))
                return Ok<Rover>(RoverContext.Rovers[RoverId.Value]);

            return BadRequest("Could not locate Rover Id " + RoverId.ToString());
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult CreateRover(Rover value)
        {
            if (!value.RoverId.HasValue)
                return BadRequest("RoverId is a required parameter");
            if (null == value.RoverName)
                return BadRequest("Rover Name is a required parameter");

            // This method only creates *new* rovers. This ID is already in use.
            if (RoverContext.Rovers.ContainsKey(value.RoverId.Value))
            {
                // Per Microsoft best practice a 400 is what is suggested for data validation errors.
                // However, a 422 is also a reasonable alternative.
                return BadRequest("Rover Id " + value.RoverId + " already exists. Please select another Rover Id and try again");
            }

            RoverContext.Rovers.Add(value.RoverId.Value, value);
            // While not in the original spec, best practice on creating a new resrouce is to return a representation of the newly created resource.
            return Created<Rover>(Request.RequestUri + value.RoverId.ToString(), RoverContext.Rovers[value.RoverId.Value]);
        }

        [Route("{roverId:int}")]
        [HttpPut]
        public IHttpActionResult RenameRover(Rover value)
        {
            // Given that the spec required passing in the rover id I'm going to use that for identifying the rover.
            // I would perfer however to actually use it from the resource name instead. That would be
            // Less prone to user error.
            if (!value.RoverId.HasValue)
                return BadRequest("RoverId is a required parameter");

            if (null == value.RoverName)
                return BadRequest("Rover Name is a required parameter");

            // This method requires a rover to already exist
            if (!RoverContext.Rovers.ContainsKey(value.RoverId.Value))
            {
                return NotFound();
            }

            RoverContext.Rovers[value.RoverId.Value].RoverName = value.RoverName;

            return Ok();
        }

        [Route("{roverId:int}/positions")]
        [HttpPut]
        public IHttpActionResult MoveRover(RoverMove value)
        {
            // Given that the spec required passing in the rover id I'm going to use that for identifying the rover.
            // I would perfer however to actually use it from the resource name instead. That would be
            // Less prone to user error.
            if (!value.RoverId.HasValue)
                return BadRequest("RoverId is a required parameter");

            if (null == value.MovementInstruction)
                return BadRequest("MovementInstruction is a required parameter");

            try
            {
                RoverContext.Rovers[value.RoverId.Value].CurrentPosition.Update(value.MovementInstruction);
                return Ok<Rover>(RoverContext.Rovers[value.RoverId.Value]);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}