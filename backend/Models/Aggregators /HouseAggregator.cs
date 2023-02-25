using System;
using backend.Models.Common;
using backend.Models.Entity;

namespace backend.Models.Aggregators
{
    public record HouseAggregator
    {

        public HouseAggregator(Guid id, DateTime createdAt, string name, string address, string city, string state, IEnumerable<Temperature> temperatures, IEnumerable<Humidity> humidities, IEnumerable<SunExposure> sunExposures)
        {
            List<Exception> exceptions = new();
            if (string.IsNullOrEmpty(name))
                exceptions.Add(new DomainException("House name is required"));
            if (string.IsNullOrEmpty(address))
                exceptions.Add(new DomainException("House address is required"));
            if (string.IsNullOrEmpty(city))
                exceptions.Add(new DomainException("House city is required"));
            if (string.IsNullOrEmpty(state))
                exceptions.Add(new DomainException("House state is required"));

            if (temperatures != null)
            {
                if (temperatures.Count() == 0)
                    exceptions.Add(new DomainException("House must have at least one temperature"));
                if (temperatures.Any(t => t.Value < -100 || t.Value > 60)) // Austrailia proved 50 is not the max and also california it seems. Antarctica is -89.2 C soo...
                    exceptions.Add(new DomainException("Temperature must be between -100 and 60"));
            }
            else
                exceptions.Add(new DomainException("House temperatures are required"));

            if (humidities != null)
            {
                if (humidities.Count() == 0)
                    exceptions.Add(new DomainException("House must have at least one humidity"));
                if (humidities.Any(h => h.Value < 0 || h.Value > 100))
                    exceptions.Add(new DomainException("Humidity must be between 0 and 100"));
            }
            else
                exceptions.Add(new DomainException("House humidities are required"));


            if (sunExposures != null)
            {
                if (sunExposures.Count() == 0)
                    exceptions.Add(new DomainException("House must have at least one sun exposure"));
                if (sunExposures.Any(s => s.Value < 0 || s.Value > 100))
                    exceptions.Add(new DomainException("Sun exposure must be between 0 and 100"));
            }
            else
                exceptions.Add(new DomainException("House sun exposures are required"));

            if (temperatures != null && humidities != null && sunExposures != null)
            {
                if (temperatures.Any(t => t.HouseId != id) || humidities.Any(h => h.HouseId != id) || sunExposures.Any(s => s.HouseId != id))
                    exceptions.Add(new DomainException("House id must be the same for all devices"));
                if (temperatures.Any(t => t.RecordedAt < createdAt) || humidities.Any(h => h.RecordedAt < createdAt) || sunExposures.Any(s => s.RecordedAt < createdAt))
                    exceptions.Add(new DomainException("All devices must have a date after the house creation"));
            }



            if (exceptions.Count > 0)
                throw new AggregateException(exceptions);

            House = new House
            {
                State = state,
                Name = name,
                Id = id,
                Address = address,
                City = city,
                CreatedAt = createdAt
            };
            // We know for a fact that all of these are not null
            // We deactive the null check for that reason
#pragma warning disable CS8601 // Dereference of a possibly null reference.
            Temperatures = temperatures;
            Humidities = humidities;
            SunExposures = sunExposures;
#pragma warning restore CS8601 // Dereference of a possibly null reference.
        }


        public House House { get; init; }
        public IEnumerable<Temperature> Temperatures { get; init; }
        public IEnumerable<Humidity> Humidities { get; init; }
        public IEnumerable<SunExposure> SunExposures { get; init; }
    }

    public class HouseResponse
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public IEnumerable<Temperature> Temperatures { get; set; }
        public IEnumerable<Humidity> Humidities { get; set; }
        public IEnumerable<SunExposure> SunExposures { get; set; }

        public HouseResponse(HouseAggregator houseAggregator)
        {
            Name = houseAggregator.House.Name;
            Address = houseAggregator.House.Address;
            City = houseAggregator.House.City;
            State = houseAggregator.House.State;
            Temperatures = houseAggregator.Temperatures;
            Humidities = houseAggregator.Humidities;
            SunExposures = houseAggregator.SunExposures;
        }

        public HouseAggregator ToHouseAggregator() => new HouseAggregator(Id, CreatedAt, Name, Address, City, State, Temperatures, Humidities, SunExposures);

    }
}



