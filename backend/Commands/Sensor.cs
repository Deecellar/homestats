using backend.Models.Entity;
using backend.Services;
using MediatR;

namespace backend.Queries
{
    public class GetAllSensorQuery : IRequest<IEnumerable<Sensor>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class GetSensorByIdQuery : IRequest<Sensor>
    {
        public GetSensorByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }

    public class SensorResponse
    {
        public SensorType Type { get; set; }
        public double Value { get; set; }
        public DateTime RecordedAt { get; set; }
        public Guid HouseId { get; set; }
    }

    public class GetAllSensorRequestHandler : IRequestHandler<GetAllSensorQuery, IEnumerable<Sensor>>
    {
        private readonly ISensorService _sensorService;

        public GetAllSensorRequestHandler(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }

        public async Task<IEnumerable<Sensor>> Handle(GetAllSensorQuery request, CancellationToken cancellationToken)
        {
            return await _sensorService.GetAllSensors(request.Page, request.PageSize);
        }
    }

    public class GetSensorByIdRequestHandler : IRequestHandler<GetSensorByIdQuery, Sensor>
    {
        private readonly ISensorService _sensorService;

        public GetSensorByIdRequestHandler(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }

        public async Task<Sensor> Handle(GetSensorByIdQuery request, CancellationToken cancellationToken)
        {
            return await _sensorService.GetSensorById(request.Id);
        }
    }
}

namespace backend.Commands
{
    public class CreateSensorCommand : IRequest<Sensor>
    {
        public CreateSensorCommand(Sensor sensor)
        {
            Sensor = sensor;
        }

        public Sensor Sensor { get; set; }
    }

    public class UpdateSensorCommand : IRequest<Sensor>
    {
        public UpdateSensorCommand(Guid id, Sensor sensor)
        {
            Id = id;
            Sensor = sensor;
        }

        public Guid Id { get; set; }
        public Sensor Sensor { get; set; }
    }

    public class DeleteSensorCommand : IRequest
    {
        public DeleteSensorCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }

    public class CreateSensorRequestHandler : IRequestHandler<CreateSensorCommand, Sensor>
    {
        private readonly ISensorService _sensorService;

        public CreateSensorRequestHandler(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }

        public async Task<Sensor> Handle(CreateSensorCommand request, CancellationToken cancellationToken)
        {
            return await _sensorService.CreateSensor(request.Sensor);
        }
    }

    public class UpdateSensorRequestHandler : IRequestHandler<UpdateSensorCommand, Sensor>
    {
        private readonly ISensorService _sensorService;

        public UpdateSensorRequestHandler(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }

        public async Task<Sensor> Handle(UpdateSensorCommand request, CancellationToken cancellationToken)
        {
            return await _sensorService.UpdateSensor(request.Id, request.Sensor);
        }
    }

    public class DeleteSensorRequestHandler : IRequestHandler<DeleteSensorCommand>
    {
        private readonly ISensorService _sensorService;

        public DeleteSensorRequestHandler(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }

        public async Task Handle(DeleteSensorCommand request, CancellationToken cancellationToken)
        {
            await _sensorService.DeleteSensor(request.Id);
        }
    }
}