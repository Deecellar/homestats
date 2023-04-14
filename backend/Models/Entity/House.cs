using backend.Models.Common;

namespace backend.Models.Entity;

public record House : EntityBase
{
    public House(string name, string address, string city, string state)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("House name cannot be null or whitespace", nameof(name));
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("House address cannot be null or whitespace", nameof(address));
        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("House city cannot be null or whitespace", nameof(city));
        if (string.IsNullOrWhiteSpace(state))
            throw new ArgumentException("House state cannot be null or whitespace", nameof(state));
        Name = name;
        Address = address;
        City = city;
        State = state;
    }

    public string Name { get; init; }
    public string Address { get; init; }
    public string City { get; init; }
    public string State { get; init; }
}