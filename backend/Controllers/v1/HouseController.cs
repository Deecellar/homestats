using backend.Commands;
using backend.Models.Aggregators;
using backend.Models.Entity;
using backend.Queries;
using backend.Wrappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers.v1;

[ApiVersion("1.0")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

public class HouseController : BaseApiController
{
    [HttpGet]
    [ProducesResponseType(typeof(Response<List<House>>), 200)]
    public async Task<ActionResult> Get([FromQuery] GetHousesQuery query)
    {
        return Ok(new Response<IEnumerable<House>>(await Mediator.Send(query)));
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(typeof(Response<House>), 200)]

    public async Task<ActionResult> Get(GetHouseByIdQuery query)
    {
        return Ok(new Response<House>(await Mediator.Send(query)));
    }

    [HttpPost]
    [ProducesResponseType(typeof(Response<House>), 200)]
    public async Task<ActionResult> Post([FromBody] CreateHouseCommand command)
    {
        return Ok(new Response<House>(await Mediator.Send(command)));
    }

    [HttpPut("{Id}")]
    [ProducesResponseType(typeof(Response<House>), 200)]

    public async Task<ActionResult> Put([FromRoute] Guid id, [FromBody] UpdateHouseCommand command)
    {
        if (id != command.Id) return BadRequest();
        return Ok(new Response<House>(await Mediator.Send(command)));
    }

    [HttpDelete("{Id}")]
    public async Task<ActionResult> Delete([FromRoute] DeleteHouseCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }

    [HttpGet("{Id}/temperatures")]
    [ProducesResponseType(typeof(Response<IEnumerable<Temperature>>),200)]
    public async Task<ActionResult> GetTemperatures( GetHouseTemperatures query)
    {
        return Ok(new Response<IEnumerable<Temperature>>(await Mediator.Send(query)));
    }
    [ProducesResponseType(typeof(Response<IEnumerable<Humidity>>),200)]

    [HttpGet("{Id}/humidity")]
    public async Task<ActionResult> GetHumidity( GetHouseHumidities query)
    {
        return Ok(new Response<IEnumerable<Humidity>>(await Mediator.Send(query)));
    }
    [ProducesResponseType(typeof(Response<IEnumerable<SunExposure>>),200)]

    [HttpGet("{Id}/sun")]
    public async Task<ActionResult> GetSun( GetHouseSunExposures query)
    {
        return Ok(new Response<IEnumerable<SunExposure>>(await Mediator.Send(query)));
    }

    [HttpGet("{Id}/sensors")]
    [ProducesResponseType(typeof(Response<IEnumerable<Sensor>>),200)]

    public async Task<ActionResult> GetSensors( GetHouseSensors query)
    {
        return Ok(new Response<IEnumerable<Sensor>>(await Mediator.Send(query)));
    }

    [HttpGet("{Id}/sensors/{sensorId}")]
    [ProducesResponseType(typeof(Response<HouseAggregator>),200)]

    public async Task<ActionResult> GetSensor( GetHouseSensorsById query)
    {
        return Ok(new Response<HouseAggregator>(await Mediator.Send(query)));
    }

    [HttpGet("sensors")]
    [ProducesResponseType(typeof(Response<IEnumerable<HouseAggregator>>),200)]

    public async Task<ActionResult> GetSensors(GetAllHouseSensors query)
    {
        return Ok(new Response<IEnumerable<HouseAggregator>>(await Mediator.Send(query)));
    }
}