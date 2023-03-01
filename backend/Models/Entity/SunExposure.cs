using System;
using backend.Models.Common;

namespace backend.Models.Entity
{
    public record SunExposure : EntityBase
    {
        public double Value { get; init; }
        public DateTime RecordedAt { get; init; }
        public Guid HouseId { get; init; }
    }
}
