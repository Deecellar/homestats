using System.Text.Json.Serialization;
using backend.Models.Aggregators;
using backend.Models.Entity;
using backend.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace backend.Queries
{
    public class GetHousesQuery : IRequest<IEnumerable<House>>
    {
        [FromQuery]
        public int Page { get; set; }
        [FromQuery]
        public int PageSize { get; set; }
    }

    public class GetHouseByIdQuery : IRequest<House>
    {
        [FromRoute]
        public Guid Id { get; set; }
    }

    public class GetAllHouseSensors : IRequest<IEnumerable<HouseAggregator>>
    {
        [FromQuery]
        public int Page { get; set; }
        [FromQuery]public int PageSize { get; set; }
        [FromQuery]public int LimitSensors { get; set; }
        [FromQuery]public int OffsetSensors { get; set; }
    }

    public class GetHouseSensorsById : IRequest<HouseAggregator>
    {
        [FromRoute]
        public Guid Id { get; set; }
        [FromQuery] public int LimitSensors { get; set; }
        [FromQuery] public int OffsetSensors { get; set; }
    }

    public class GetHouseSensors : IRequest<IEnumerable<Sensor>>
    {
        [FromRoute]
        public Guid Id { get; set; }
        [FromRoute]
        public int LimitSensors { get; set; }
        [FromRoute]
        public int OffsetSensors { get; set; }
    }

    public class GetHouseTemperatures : IRequest<IEnumerable<Temperature>>
    {
     [FromRoute]   public Guid Id { get; set; }
        [FromRoute]public int LimitSensors { get; set; }
        [FromRoute]public int OffsetSensors { get; set; }
    }

    public class GetHouseHumidities : IRequest<IEnumerable<Humidity>>
    {
        [FromRoute]
        public Guid Id { get; set; }
        [FromQuery]
        public int LimitSensors { get; set; }
        [FromQuery]
        public int OffsetSensors { get; set; }
    }

    public class GetHouseSunExposures : IRequest<IEnumerable<SunExposure>>
    {
        [FromRoute]
        public Guid Id { get; set; }
        [FromQuery] public int LimitSensors { get; set; }
        [FromQuery] public int OffsetSensors { get; set; }
    }

    public class GetHouseByIdQueryHandler : IRequestHandler<GetHouseByIdQuery, House>
    {
        private readonly IHouseService _houseService;

        public GetHouseByIdQueryHandler(IHouseService houseService)
        {
            _houseService = houseService;
        }

        public async Task<House> Handle(GetHouseByIdQuery request, CancellationToken cancellationToken)
        {
            return await _houseService.GetHouseById(request.Id);
        }
    }

    public class GetHousesQueryHandler : IRequestHandler<GetHousesQuery, IEnumerable<House>>
    {
        private readonly IHouseService _houseService;

        public GetHousesQueryHandler(IHouseService houseService)
        {
            _houseService = houseService;
        }

        public async Task<IEnumerable<House>> Handle(GetHousesQuery request, CancellationToken cancellationToken)
        {
            return await _houseService.GetAllHouses(request.Page, request.PageSize);
        }
    }

    public class GetAllHouseSensorsHandler : IRequestHandler<GetAllHouseSensors, IEnumerable<HouseAggregator>>
    {
        private readonly IHouseService _houseService;

        public GetAllHouseSensorsHandler(IHouseService houseService)
        {
            _houseService = houseService;
        }

        public async Task<IEnumerable<HouseAggregator>> Handle(GetAllHouseSensors request,
            CancellationToken cancellationToken)
        {
            return await _houseService.GetAllHouseSensors(request.Page, request.PageSize, request.LimitSensors,
                request.OffsetSensors);
        }
    }

    public class GetHouseSensorsByIdHandler : IRequestHandler<GetHouseSensorsById, HouseAggregator>
    {
        private readonly IHouseService _houseService;

        public GetHouseSensorsByIdHandler(IHouseService houseService)
        {
            _houseService = houseService;
        }

        public async Task<HouseAggregator> Handle(GetHouseSensorsById request, CancellationToken cancellationToken)
        {
            return await _houseService.GetHouseSensorsById(request.Id, request.LimitSensors, request.OffsetSensors);
        }
    }

    public class GetHouseSensorsHandler : IRequestHandler<GetHouseSensors, IEnumerable<Sensor>>
    {
        private readonly IHouseService _houseService;

        public GetHouseSensorsHandler(IHouseService houseService)
        {
            _houseService = houseService;
        }

        public async Task<IEnumerable<Sensor>> Handle(GetHouseSensors request, CancellationToken cancellationToken)
        {
            return await _houseService.GetHouseSensors(request.Id, request.LimitSensors, request.OffsetSensors);
        }
    }

    public class GetHouseTemperaturesHandler : IRequestHandler<GetHouseTemperatures, IEnumerable<Temperature>>
    {
        private readonly IHouseService _houseService;

        public GetHouseTemperaturesHandler(IHouseService houseService)
        {
            _houseService = houseService;
        }

        public async Task<IEnumerable<Temperature>> Handle(GetHouseTemperatures request,
            CancellationToken cancellationToken)
        {
            return await _houseService.GetHouseTemperatures(request.Id, request.LimitSensors, request.OffsetSensors);
        }
    }

    public class GetHouseHumiditiesHandler : IRequestHandler<GetHouseHumidities, IEnumerable<Humidity>>
    {
        private readonly IHouseService _houseService;

        public GetHouseHumiditiesHandler(IHouseService houseService)
        {
            _houseService = houseService;
        }

        public async Task<IEnumerable<Humidity>> Handle(GetHouseHumidities request, CancellationToken cancellationToken)
        {
            return await _houseService.GetHouseHumidities(request.Id, request.LimitSensors, request.OffsetSensors);
        }
    }

    public class GetHouseSunExposuresHandler : IRequestHandler<GetHouseSunExposures, IEnumerable<SunExposure>>
    {
        private readonly IHouseService _houseService;

        public GetHouseSunExposuresHandler(IHouseService houseService)
        {
            _houseService = houseService;
        }

        public async Task<IEnumerable<SunExposure>> Handle(GetHouseSunExposures request,
            CancellationToken cancellationToken)
        {
            return await _houseService.GetHouseSunExposures(request.Id, request.LimitSensors, request.OffsetSensors);
        }
    }
}

namespace backend.Commands
{
    public class CreateHouseCommand : IRequest<House>
    {
        public CreateHouseCommand(House house)
        {
            if (house == null)
                throw new ArgumentNullException(nameof(house));
            this.House = house;
        }

        public House House { get; }
    }

    public class UpdateHouseCommand : IRequest<House>

    {

        public Guid Id { get; set; }
        public House House { get; set; }
    }

    public class DeleteHouseCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class CreateHouseCommandHandler : IRequestHandler<CreateHouseCommand, House>
    {
        private readonly IHouseService _houseService;

        public CreateHouseCommandHandler(IHouseService houseService)
        {
            _houseService = houseService;
        }

        public async Task<House> Handle(CreateHouseCommand request, CancellationToken cancellationToken)
        {
            return await _houseService.CreateHouse(request.House);
        }
    }

    public class UpdateHouseCommandHandler : IRequestHandler<UpdateHouseCommand, House>
    {
        private readonly IHouseService _houseService;

        public UpdateHouseCommandHandler(IHouseService houseService)
        {
            _houseService = houseService;
        }

        public async Task<House> Handle(UpdateHouseCommand request, CancellationToken cancellationToken)
        {
            return await _houseService.UpdateHouse(request.Id, request.House);
        }
    }

    public class DeleteHouseCommandHandler : IRequestHandler<DeleteHouseCommand>
    {
        private readonly IHouseService _houseService;

        public DeleteHouseCommandHandler(IHouseService houseService)
        {
            _houseService = houseService;
        }

        public async Task Handle(DeleteHouseCommand request, CancellationToken cancellationToken)
        {
            await _houseService.DeleteHouse(request.Id);
        }
    }
}