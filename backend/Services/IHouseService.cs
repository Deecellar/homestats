using System;
using backend.Models.Aggregators;
using backend.Models.Entity;
namespace backend.Services
{
    public interface IHouseService
    {
        Task<IReadOnlyCollection<House>> GetAllHouses(int page, int pageSize);
        Task<House> GetHouseById(Guid id);
        Task<House> CreateHouse(House house);
        Task<House> UpdateHouse(Guid id, HouseAggregator house);
        Task DeleteHouse(Guid id);

        Task<IReadOnlyCollection<HouseAggregator>> GetAllHouseSensors(int page, int pageSize, int limit_sensors, int offset_sensors);
        Task<HouseAggregator> GetHouseSensorsById(Guid id, int limit_sensors, int offset_sensors);
        Task<IReadOnlyCollection<Sensor>> GetHouseSensors(Guid id, int limit_sensors, int offset_sensors);
        Task<IReadOnlyCollection<Temperature>> GetHouseTemperatures(Guid id, int limit_sensors, int offset_sensors);
        Task<IReadOnlyCollection<Humidity>> GetHouseHumidities(Guid id, int limit_sensors, int offset_sensors);
        Task<IReadOnlyCollection<SunExposure>> GetHouseSunExposures(Guid id, int limit_sensors, int offset_sensors);




    }
}
