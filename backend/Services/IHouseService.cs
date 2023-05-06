using backend.Models.Aggregators;
using backend.Models.Entity;

namespace backend.Services;

public interface IHouseService
{
    Task<IReadOnlyCollection<House>> GetAllHouses(int page, int pageSize);
    Task<House> GetHouseById(Guid id);
    Task<House> CreateHouse(House house);
    Task<House> UpdateHouse(Guid id, House house);
    Task DeleteHouse(Guid id);

    Task<IReadOnlyCollection<HouseAggregator>> GetAllHouseSensors(int page, int pageSize, int limitSensors,
        int offsetSensors);

    Task<HouseAggregator> GetHouseSensorsById(Guid id, int limitSensors, int offsetSensors);
    Task<IReadOnlyCollection<Sensor>> GetHouseSensors(Guid id, int limitSensors, int offsetSensors);
    Task<IReadOnlyCollection<Temperature>> GetHouseTemperatures(Guid id, int limitSensors, int offsetSensors);
    Task<IReadOnlyCollection<Humidity>> GetHouseHumidities(Guid id, int limitSensors, int offsetSensors);
    Task<IReadOnlyCollection<SunExposure>> GetHouseSunExposures(Guid id, int limitSensors, int offsetSensors);
}