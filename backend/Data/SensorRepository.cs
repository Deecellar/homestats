using System.Collections.ObjectModel;
using backend.Models.Aggregators;
using backend.Models.Entity;
using LiteDB;

namespace backend.Data;

public class SensorRepository : GenericRepository<Sensor>, ISensorRepository
{
    public SensorRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public Task<IReadOnlyCollection<Sensor>> GetByHouseIdAsync(Guid id, int offset_sensors, int limit_sensors)
    {
        var sensors = Collection.Find(Query.EQ("HouseId", id), offset_sensors, limit_sensors).ToList();
        return Task.FromResult<IReadOnlyCollection<Sensor>>(new ReadOnlyCollection<Sensor>(sensors).ToList());
    }

    public async Task<HouseAggregator> GetHouseSensorsByIdAsync(Guid id, int offset_sensors, int limit_sensors)
    {
        var house = await UnitOfWork.HouseRepository.GetByIdAsync(id);
        var sensors = Collection.Find(Query.EQ("HouseId", id), offset_sensors, limit_sensors);
        var houseAggregator = new HouseAggregator(
            house,
            sensors.Where(x => x.Type == SensorType.Temperature).Select(x => new Temperature
            {
                Id = x.Id,
                CreatedAt = x.CreatedAt,
                HouseId = x.HouseId,
                Value = x.Value
            }).ToList(),
            sensors.Where(x => x.Type == SensorType.Humidity).Select(x => new Humidity
            {
                Id = x.Id,
                CreatedAt = x.CreatedAt,
                HouseId = x.HouseId,
                Value = x.Value
            }).ToList(),
            sensors.Where(x => x.Type == SensorType.SunExposure).Select(x => new SunExposure
            {
                Id = x.Id,
                CreatedAt = x.CreatedAt,
                HouseId = x.HouseId,
                Value = x.Value
            }).ToList()
        );
        return houseAggregator;
    }

    public Task<IReadOnlyCollection<Humidity>> GetHumidityByHouseIdAsync(Guid id, int page, int limit)
    {
        var sensors = Collection
            .Find(Query.And(Query.EQ("SensorType", Enum.GetName(SensorType.Humidity)), Query.EQ("HouseId", id)), page,
                limit).ToList();
        var humidities = sensors.Select(x => new Humidity
        {
            Id = x.Id,
            CreatedAt = x.CreatedAt,
            HouseId = x.HouseId,
            Value = x.Value
        }).ToList();
        return Task.FromResult<IReadOnlyCollection<Humidity>>(new ReadOnlyCollection<Humidity>(humidities).ToList());
    }

    public Task<IReadOnlyCollection<SunExposure>> GetSunExposureByHouseIdAsync(Guid id, int page, int limit)
    {
        var sensors = Collection
            .Find(Query.And(Query.EQ("SensorType", Enum.GetName(SensorType.SunExposure)), Query.EQ("HouseId", id)),
                page, limit).ToList();
        var sunExposures = sensors.Select(x => new SunExposure
        {
            Id = x.Id,
            CreatedAt = x.CreatedAt,
            HouseId = x.HouseId,
            Value = x.Value
        }).ToList();
        return Task.FromResult<IReadOnlyCollection<SunExposure>>(new ReadOnlyCollection<SunExposure>(sunExposures)
            .ToList());
    }

    public Task<IReadOnlyCollection<Temperature>> GetTemperatureByHouseIdAsync(Guid id, int page, int limit)
    {
        var sensors = Collection
            .Find(Query.And(Query.EQ("SensorType", Enum.GetName(SensorType.Temperature)), Query.EQ("HouseId", id)),
                page, limit).ToList();
        var temperatures = sensors.Select(x => new Temperature
        {
            Id = x.Id,
            CreatedAt = x.CreatedAt,
            HouseId = x.HouseId,
            Value = x.Value
        }).ToList();
        return Task.FromResult<IReadOnlyCollection<Temperature>>(new ReadOnlyCollection<Temperature>(temperatures)
            .ToList());
    }
}