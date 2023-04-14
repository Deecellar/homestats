using backend.Models.Aggregators;
using backend.Models.Entity;

namespace backend.Data;

public interface ISensorRepository : IRepository<Sensor>
{
    Task<IReadOnlyCollection<Temperature>> GetTemperatureByHouseIdAsync(Guid id, int page, int limit);
    Task<IReadOnlyCollection<Humidity>> GetHumidityByHouseIdAsync(Guid id, int page, int limit);
    Task<IReadOnlyCollection<SunExposure>> GetSunExposureByHouseIdAsync(Guid id, int page, int limit);
    Task<IReadOnlyCollection<Sensor>> GetByHouseIdAsync(Guid id, int offset_sensors, int limit_sensors);
    Task<HouseAggregator> GetHouseSensorsByIdAsync(Guid id, int offset_sensors, int limit_sensors);
}