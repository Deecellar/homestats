using System;
using backend.Models.Aggregators;
using backend.Models.Entity;
namespace backend.Services
{
    public interface IHouseService
    {
        Task<IEnumerable<House>> GetAllHouses(int page, int pageSize);
        Task<House> GetHouseById(Guid id);
        Task<House> CreateHouse(House house);
        Task<House> UpdateHouse(Guid id, HouseAggregator house);
        Task DeleteHouse(Guid id);

        Task<IEnumerable<HouseAggregator>> GetAllHouseSensors(int page, int pageSize, int limit_sensors, int offset_sensors);
        Task<HouseAggregator> GetHouseSensorsById(Guid id, int limit_sensors, int offset_sensors);
        Task<IEnumerable<Sensor>> GetHouseSensors(Guid id, int limit_sensors, int offset_sensors);
        Task<IEnumerable<Temperature>> GetHouseTemperatures(Guid id, int limit_sensors, int offset_sensors);
        Task<IEnumerable<Humidity>> GetHouseHumidities(Guid id, int limit_sensors, int offset_sensors);
        Task<IEnumerable<SunExposure>> GetHouseSunExposures(Guid id, int limit_sensors, int offset_sensors);




    }
}
