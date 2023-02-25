using System;
using backend.Models.Aggregators;
using backend.Models.Entity;

namespace backend.Services.Implementations
{
    public class HouseService : IHouseService
    {

        public Task<House> CreateHouse(House house)
        {
            throw new NotImplementedException();
        }

        public Task DeleteHouse(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<House>> GetAllHouses(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<HouseAggregator>> GetAllHouseSensors(int page, int pageSize, int limit_sensors, int offset_sensors)
        {
            throw new NotImplementedException();
        }

        public Task<House> GetHouseById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Humidity>> GetHouseHumidities(Guid id, int limit_sensors, int offset_sensors)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Sensor>> GetHouseSensors(Guid id, int limit_sensors, int offset_sensors)
        {
            throw new NotImplementedException();
        }

        public Task<HouseAggregator> GetHouseSensorsById(Guid id, int limit_sensors, int offset_sensors)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SunExposure>> GetHouseSunExposures(Guid id, int limit_sensors, int offset_sensors)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Temperature>> GetHouseTemperatures(Guid id, int limit_sensors, int offset_sensors)
        {
            throw new NotImplementedException();
        }

        public Task<House> UpdateHouse(Guid id, HouseAggregator house)
        {
            throw new NotImplementedException();
        }
    }
}
