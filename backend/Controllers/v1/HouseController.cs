using backend.Commands;
using backend.Queries;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers.v1;

[ApiVersion("1.0")]
public class HouseController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult> Get([FromQuery] GetHousesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult> Get([FromRoute] GetHouseByIdQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateHouseCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put([FromRoute] Guid id, [FromBody] UpdateHouseCommand command)
    {
        if (id != command.Id) return BadRequest();
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] DeleteHouseCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }

    [HttpGet("{Id}/temperatures")]
    public async Task<ActionResult> GetTemperatures([FromRoute] GetHouseTemperatures query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{Id}/humidity")]
    public async Task<ActionResult> GetHumidity([FromRoute] GetHouseHumidities query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{Id}/sun")]
    public async Task<ActionResult> GetSun([FromRoute] GetHouseSunExposures query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{Id}/sensors")]
    public async Task<ActionResult> GetSensors([FromRoute] GetHouseSensors query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{Id}/sensors/{sensorId}")]
    public async Task<ActionResult> GetSensor([FromRoute] GetHouseSensorsById query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("sensors")]
    public async Task<ActionResult> GetSensors([FromQuery] GetAllHouseSensors query)
    {
        return Ok(await Mediator.Send(query));
    }
}