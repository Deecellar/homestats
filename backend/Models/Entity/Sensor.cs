using System;
using backend.Models.Common;

namespace backend.Models.Entity
{
    public record Sensor : EntityBase
    {
        public SensorType Type { get; init; }
        public double Value { get; init; }
        public DateTime RecordedAt { get; init; }

        public Guid HouseId { get; init; }
    }

    public enum SensorType
    {
        Temperature,
        Humidity,
        SunExposure
    }
}
