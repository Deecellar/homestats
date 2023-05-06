using backend.Data;
using backend.Models.Aggregators;
using backend.Models.Entity;

namespace backend.Services.Implementations;

public class HouseService : IHouseService
{
    private readonly IAuthenticatedUserService _authenticatedUserService;
    private readonly IHouseRepository _houseRepository;
    private readonly ISensorRepository _sensorRepository;

    public HouseService(IUnitOfWork unitOfWork, IAuthenticatedUserService authenticatedUserService)
    {
        _houseRepository = unitOfWork.HouseRepository;
        _authenticatedUserService = authenticatedUserService;
        _sensorRepository = unitOfWork.SensorRepository;
    }

    public Task<House> CreateHouse(House house)
    {
        if (_authenticatedUserService.UserId == null)
            throw new UnauthorizedAccessException("You are not authorized to create a house");
        // We add the current date to the house, need to recreate it since it's a record
        var newHouse = new House(house.Name, house.Address, house.City, house.State)
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.Now
        };
        return _houseRepository.AddAsync(newHouse);
    }

    public Task DeleteHouse(Guid id)
    {
        if (_authenticatedUserService.UserId == null)
            throw new UnauthorizedAccessException("You are not authorized to delete a house");

        return _houseRepository.DeleteAsync(id);
    }

    public Task<IReadOnlyCollection<House>> GetAllHouses(int page, int pageSize)
    {
        if (_authenticatedUserService.UserId == null)
            throw new UnauthorizedAccessException("You are not authorized to get all houses");

        return _houseRepository.GetAllAsync();
    }

    public async Task<IReadOnlyCollection<HouseAggregator>> GetAllHouseSensors(int page, int pageSize,
        int limit_sensors, int offset_sensors)
    {
        if (_authenticatedUserService.UserId == null)
            throw new UnauthorizedAccessException("You are not authorized to get all houses");

        var houses = await _houseRepository.GetPagedAsync(page, pageSize);
        var houseAggregators = new List<HouseAggregator>();
        foreach (var house in houses)
        {
            var houseAggregator = new HouseAggregator(house,
                await _sensorRepository.GetTemperatureByHouseIdAsync(house.Id, offset_sensors, limit_sensors),
                await _sensorRepository.GetHumidityByHouseIdAsync(house.Id, offset_sensors, limit_sensors),
                await _sensorRepository.GetSunExposureByHouseIdAsync(house.Id, offset_sensors, limit_sensors));
            houseAggregators.Add(houseAggregator);
        }

        return houseAggregators;
    }

    public Task<House> GetHouseById(Guid id)
    {
        if (_authenticatedUserService.UserId == null)
            throw new UnauthorizedAccessException("You are not authorized to get a house");

        return _houseRepository.GetByIdAsync(id);
    }

    public Task<IReadOnlyCollection<Humidity>> GetHouseHumidities(Guid id, int limit_sensors, int offset_sensors)
    {
        if (_authenticatedUserService.UserId == null)
            throw new UnauthorizedAccessException("You are not authorized to get a house");
        var house = _houseRepository.GetByIdAsync(id);
        if (house == null)
            throw new UnauthorizedAccessException("House not found");
        return _sensorRepository.GetHumidityByHouseIdAsync(id, offset_sensors, limit_sensors);
    }

    public Task<IReadOnlyCollection<Sensor>> GetHouseSensors(Guid id, int limit_sensors, int offset_sensors)
    {
        if (_authenticatedUserService.UserId == null)
            throw new UnauthorizedAccessException("You are not authorized to get a house");
        var house = _houseRepository.GetByIdAsync(id);
        if (house == null)
            throw new UnauthorizedAccessException("House not found");
        return _sensorRepository.GetByHouseIdAsync(id, offset_sensors, limit_sensors);
    }

    public Task<HouseAggregator> GetHouseSensorsById(Guid id, int limit_sensors, int offset_sensors)
    {
        if (_authenticatedUserService.UserId == null)
            throw new UnauthorizedAccessException("You are not authorized to get a house");
        var house = _houseRepository.GetByIdAsync(id);
        if (house == null)
            throw new UnauthorizedAccessException("House not found");
        return _sensorRepository.GetHouseSensorsByIdAsync(id, offset_sensors, limit_sensors);
    }

    public Task<IReadOnlyCollection<SunExposure>> GetHouseSunExposures(Guid id, int limit_sensors, int offset_sensors)
    {
        if (_authenticatedUserService.UserId == null)
            throw new UnauthorizedAccessException("You are not authorized to get a house");
        var house = _houseRepository.GetByIdAsync(id);
        if (house == null)
            throw new UnauthorizedAccessException("House not found");
        return _sensorRepository.GetSunExposureByHouseIdAsync(id, offset_sensors, limit_sensors);
    }

    public Task<IReadOnlyCollection<Temperature>> GetHouseTemperatures(Guid id, int limit_sensors, int offset_sensors)
    {
        if (_authenticatedUserService.UserId == null)
            throw new UnauthorizedAccessException("You are not authorized to get a house");
        var house = _houseRepository.GetByIdAsync(id);
        if (house == null)
            throw new UnauthorizedAccessException("House not found");
        return _sensorRepository.GetTemperatureByHouseIdAsync(id, offset_sensors, limit_sensors);
    }

    public async Task<House> UpdateHouse(Guid id, House house)
    {
        if (_authenticatedUserService.UserId == null)
            throw new UnauthorizedAccessException("You are not authorized to update a house");
        var houseToUpdate = await _houseRepository.GetByIdAsync(id);
        if (houseToUpdate == null)
            throw new UnauthorizedAccessException("House not found");

        var newHome = new House(house.Name, house.Address, house.City, house.State)
        {
            Id = id,
            CreatedAt = houseToUpdate.CreatedAt
        };
        await _houseRepository.UpdateAsync(newHome);
        return newHome;
    }
}