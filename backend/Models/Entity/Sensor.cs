using System.Text.Json.Serialization;
using backend.Models.Common;

namespace backend.Models.Entity;

public record Sensor : EntityBase
{
    public SensorType Type { get; init; }
    public double Value { get; init; }
    public DateTime RecordedAt { get; init; }

    public Guid HouseId { get; init; }
[JsonConstructor]
    public Sensor(SensorType type, double value, DateTime recordedAt, Guid houseId)
    {
        Type = type;
        Value = value;
        RecordedAt = recordedAt;
        HouseId = houseId;
    }

    public Sensor(Guid id, DateTime createdAt, SensorType type, double value, DateTime recordedAt, Guid houseId)
    {
        Id = id;
        CreatedAt = createdAt;
        Type = type;
        Value = value;
        RecordedAt = recordedAt;
        HouseId = houseId;
    }

    public Sensor()
    {
    }
}

public enum SensorType
{
    Temperature,
    Humidity,
    SunExposure
}