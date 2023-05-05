using System.Collections.ObjectModel;
using backend.Models.Aggregators;
using backend.Models.Entity;
using LiteDB;
using SqlKata.Execution;

namespace backend.Data;

public class SensorRepository : GenericRepository<Sensor>, ISensorRepository
{
    public SensorRepository(IUnitOfWork unitOfWork, QueryFactory queryFactory = null) : base(unitOfWork, "Sensor", queryFactory)
    {
    }

    public Task<IReadOnlyCollection<Sensor>> GetByHouseIdAsync(Guid id, int offset_sensors, int limit_sensors)
    {
        if (_isLiteDb)
        {
            var sensors = Collection.Find(Query.EQ("HouseId", id), offset_sensors, limit_sensors).ToList();
            return Task.FromResult<IReadOnlyCollection<Sensor>>(new ReadOnlyCollection<Sensor>(sensors).ToList());
        }
        else
        {
            var sensors = _compiler.Query(_tableName).Where("HouseId", id).Paginate<Sensor>(offset_sensors, limit_sensors, _unitOfWorkSqlKata?._transaction).List.ToList();
            return Task.FromResult<IReadOnlyCollection<Sensor>>(new ReadOnlyCollection<Sensor>(sensors));
        }

    }

    public async Task<HouseAggregator> GetHouseSensorsByIdAsync(Guid id, int offset_sensors, int limit_sensors)
    {
        var house = await UnitOfWork.HouseRepository.GetByIdAsync(id);
        if (_isLiteDb)
        {
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
        else
        {
            var sensors = _compiler.Query(_tableName).Where("HouseId", id).Paginate<Sensor>(offset_sensors, limit_sensors, _unitOfWorkSqlKata?._transaction).List.ToList();
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
    }

    public Task<IReadOnlyCollection<Humidity>> GetHumidityByHouseIdAsync(Guid id, int page, int limit)
    {
        if (_isLiteDb)
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
        else
        {
            var sensors = _compiler.Query(_tableName).Where("SensorType", Enum.GetName(SensorType.Humidity)).Where("HouseId", id).Paginate<Sensor>(page, limit,_unitOfWorkSqlKata?._transaction).List.ToList();
            var humidities = sensors.Select(x => new Humidity
            {
                Id = x.Id,
                CreatedAt = x.CreatedAt,
                HouseId = x.HouseId,
                Value = x.Value
            }).ToList();
            return Task.FromResult<IReadOnlyCollection<Humidity>>(new ReadOnlyCollection<Humidity>(humidities).ToList());
        }

    }

    public Task<IReadOnlyCollection<SunExposure>> GetSunExposureByHouseIdAsync(Guid id, int page, int limit)
    {
        if (_isLiteDb)
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
        else
        {
            var sensors = _compiler.Query(_tableName).Where("SensorType", Enum.GetName(SensorType.SunExposure)).Where("HouseId", id).Paginate<Sensor>(page, limit,_unitOfWorkSqlKata?._transaction).List.ToList();
            var sunExposures = sensors.Select(x => new SunExposure
            {
                Id = x.Id,
                CreatedAt = x.CreatedAt,
                HouseId = x.HouseId,
                Value = x.Value
            }).ToList();
            return Task.FromResult<IReadOnlyCollection<SunExposure>>(new ReadOnlyCollection<SunExposure>(sunExposures).ToList());
        }
    }

    public Task<IReadOnlyCollection<Temperature>> GetTemperatureByHouseIdAsync(Guid id, int page, int limit)
    {
        if (_isLiteDb)
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
        else
        {
            var sensors = _compiler.Query(_tableName).Where("SensorType", Enum.GetName(SensorType.Temperature)).Where("HouseId", id).Paginate<Sensor>(page, limit,_unitOfWorkSqlKata?._transaction).List.ToList();
            var temperatures = sensors.Select(x => new Temperature
            {
                Id = x.Id,
                CreatedAt = x.CreatedAt,
                HouseId = x.HouseId,
                Value = x.Value
            }).ToList();
            return Task.FromResult<IReadOnlyCollection<Temperature>>(new ReadOnlyCollection<Temperature>(temperatures).ToList());
        }
    }
}