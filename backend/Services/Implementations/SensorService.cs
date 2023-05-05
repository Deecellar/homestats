using backend.Data;
using backend.Models.Entity;

namespace backend.Services.Implementations;

public class SensorService : ISensorService
{
    private readonly IAuthenticatedUserService _authenticatedUserService;

    private readonly ISensorRepository _sensorRepository;

    public SensorService(IUnitOfWork unitOfWork, IAuthenticatedUserService authenticatedUserService)
    {
        _authenticatedUserService = authenticatedUserService;
        _sensorRepository = unitOfWork.SensorRepository;
    }

    public Task<Sensor> CreateSensor(Sensor sensor)
    {
        if (_authenticatedUserService.UserId == null) throw new UnauthorizedAccessException("You are not logged in");
        var newSensor = new Sensor
        {
            Id = Guid.NewGuid(),
            Type = sensor.Type,
            HouseId = sensor.HouseId,
            CreatedAt = DateTime.Now,
            RecordedAt = DateTime.Now,
            Value = sensor.Value
        };
        return _sensorRepository.AddAsync(newSensor);
    }

    public Task DeleteSensor(Guid id)
    {
        if (_authenticatedUserService.UserId == null) throw new UnauthorizedAccessException("You are not logged in");
        return _sensorRepository.DeleteAsync(id);
    }

    public Task<IReadOnlyCollection<Sensor>> GetAllSensors(int page, int pageSize)
    {
        if (_authenticatedUserService.UserId == null) throw new UnauthorizedAccessException("You are not logged in");
        return _sensorRepository.GetPagedAsync(page, pageSize);
    }

    public Task<Sensor> GetSensorById(Guid id)
    {
        if (_authenticatedUserService.UserId == null) throw new UnauthorizedAccessException("You are not logged in");
        return _sensorRepository.GetByIdAsync(id);
    }

    public Task<Sensor> UpdateSensor(Guid id, Sensor sensor)
    {
        if (_authenticatedUserService.UserId == null) throw new UnauthorizedAccessException("You are not logged in");
        var updatedSensor = new Sensor
        {
            Id = id,
            Type = sensor.Type,
            HouseId = sensor.HouseId,
            RecordedAt = DateTime.Now,
            Value = sensor.Value
        };
        _sensorRepository.UpdateAsync(updatedSensor);
        return _sensorRepository.GetByIdAsync(id);
    }
}