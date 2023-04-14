using backend.Commands;
using backend.Models.Entity;
using backend.Queries;
using backend.Wrappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers.v1;

// [ApiController]
// [Route("api/v{version:apiVersion}/[controller]")]
// <summary>
//     This is the controller for the sensors. registers all data of humidity, temperature, sun exposure, etc.
// </summary>
[ApiVersion("1.0")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class SensorController : BaseApiController
{
    [HttpGet]
    [ProducesResponseType(typeof(Response<IEnumerable<Sensor>>), 200)]
    public async Task<ActionResult> Get([FromQuery] GetAllSensorQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(typeof(Response<Sensor>), 200)]

    public async Task<ActionResult> Get([FromRoute] GetSensorByIdQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    [ProducesResponseType(typeof(Response<Sensor>), 200)]
    [AllowAnonymous]

    public async Task<ActionResult> Post([FromBody] CreateSensorCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Response<Sensor>), 200)]
    public async Task<ActionResult> Put([FromRoute] Guid id, [FromBody] UpdateSensorCommand command)
    {
        if (id != command.Id) return BadRequest();
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{Id}")]
    public async Task<ActionResult> Delete([FromRoute] DeleteSensorCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}