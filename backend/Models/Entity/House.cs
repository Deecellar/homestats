using System;
using backend.Models.Common;

namespace backend.Models.Entity
{
    public record House : EntityBase
    {
        public string Name { get; init; }
        public string Address { get; init; }
        public string City { get; init; }
        public string State { get; init; }
    }
}
