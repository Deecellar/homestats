using backend.Data;
using backend.Models.Entity;

namespace backend.Services.Implementations;

public class SensorService : ISensorService
{
    private readonly IAuthenticatedUserService _authenticatedUserService;

    private readonly ISensorRepository _sensorRepository;
    private readonly IHouseRepository _houseRepository;

    public SensorService(IUnitOfWork unitOfWork, IAuthenticatedUserService authenticatedUserService)
    {
        _authenticatedUserService = authenticatedUserService;
        _sensorRepository = unitOfWork.SensorRepository;
        _houseRepository = unitOfWork.HouseRepository;
    }

    public async Task<Sensor> CreateSensor(Sensor sensor)
    {

        // We get a random tryp from the enum
        var random = new Random();
        var values = Enum.GetValues(typeof(SensorType));
        var randomType = ((SensorType?)values.GetValue(random.Next(values.Length))) ?? SensorType.Temperature;
        var randomValue = random.NextDouble() * 100;
        IReadOnlyCollection<House> randomHouse  = await _houseRepository.GetAllAsync();
        var randomHouseId = randomHouse.ElementAt(random.Next(randomHouse.Count)).Id;
        // We get a random house id
        var newSensor = new Sensor
        {
            Id = Guid.NewGuid(),
            Type = randomType,
            HouseId = randomHouseId,
            CreatedAt = DateTime.Now,
            RecordedAt = DateTime.Now,
            Value = randomValue,
        };
        return await _sensorRepository.AddAsync(newSensor);
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

    public async Task<IReadOnlyCollection<Sensor>> GetSensorById(Guid id)
    {
        if (_authenticatedUserService.UserId == null) throw new UnauthorizedAccessException("You are not logged in");
        var all = await _sensorRepository.GetAllAsync();
        return all.Where(x => x.HouseId == id).ToList();
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