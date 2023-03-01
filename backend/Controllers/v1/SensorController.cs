using System;
using backend.Commands;
using backend.Queries;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers.v1
{
    // [ApiController]
    // [Route("api/v{version:apiVersion}/[controller]")]
    // <summary>
    //     This is the controller for the sensors. registers all data of humidity, temperature, sun exposure, etc.
    // </summary>
    [ApiVersion("1.0")]
    public class SensorController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] GetAllSensorQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> Get([FromRoute] GetSensorByIdQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateSensorCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromRoute] Guid id, [FromBody] UpdateSensorCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] DeleteSensorCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }

    }
}
