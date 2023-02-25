using System;
using backend.Models.Common;

namespace backend.Models.Entity
{
    public record Sensor 
    {
        public SensorType Type { get; init; }
        public double Value { get; init; }
        public Guid HouseId { get; init; }
    }

    public enum SensorType
    {
        Temperature,
        Humidity,
        SunExposure
    }
}
