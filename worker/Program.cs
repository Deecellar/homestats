using System;
using RestSharp;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using RestSharp.Authenticators;

// Tries to make login at the server if it fails it registers and then tries to login again
// If it fails again it will throw an exception
// If it succeeds it will get the token and then it will try to get the list of houses
// Then it will generate data for each sensor and simulate a IOT device

public class Response<T>
{
    public Response()
    {
        Succeeded = false;
    }

    public Response(T data)
    {
        Succeeded = true;
        Data = data;
    }

    public Response(List<string> errors)
    {
        Succeeded = false;
        Errors = errors;
    }

    public Response(string message, List<string> errors)
    {
        Succeeded = false;
        Message = message;
        Errors = errors;
    }

    public Response(string message, T data)
    {
        Succeeded = true;
        Message = message;
        Data = data;
    }

    public Response(string message)
    {
        Succeeded = false;
        Message = message;
    }

    public bool Succeeded { get; set; }
    public string? Message { get; set; }
    public List<string>? Errors { get; set; }
    public T? Data { get; set; }
}


public record Sensor 
{
        public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public SensorType Type { get; init; }
    public double Value { get; init; }
    public DateTime RecordedAt { get; init; }

    public Guid HouseId { get; init; }
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

public record House {
        public string Name { get; init; }
    public string Address { get; init; }
    public string City { get; init; }
    public string State { get; init; }

    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public House(string name, string address, string city, string state)
    {
        Name = name;
        Address = address;
        City = city;
        State = state;
    }

}

public class AuthenticationRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class AuthenticationResponse
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public List<string> Roles { get; set; }
    public bool IsVerified { get; set; }
    public string JWToken { get; set; }

}

public class RegisterRequest
{
     public string FirstName { get; set; }

     public string LastName { get; set; }

     public string Email { get; set; }

     public string UserName { get; set; }

     public string Password { get; set; }

     public string ConfirmPassword { get; set; }
}

public class HouseAuthenticator : AuthenticatorBase {
    
    readonly string _token;
    readonly string _baseUrl;

    public HouseAuthenticator(string token, string baseUrl) : base("")
    {
        _token = token;
        _baseUrl = baseUrl;
    }

    protected override async ValueTask<Parameter> GetAuthenticationParameter(string accessToken)
    {
        Token = string.IsNullOrEmpty(Token) ? await GetToken() : Token;
        return new HeaderParameter(KnownHeaders.Authorization, "Bearer " + Token);
    }

    async Task<string> GetToken() {
    var options = new RestClientOptions(_baseUrl);
    using var client = new RestClient(options);
    var request = new RestRequest("api/Account/authenticate");
    request.AddJsonBody(new AuthenticationRequest { Email = "fake@fake.com", Password = "fake" });
    var response = await client.PostAsync<Response<AuthenticationResponse>>(request);
    return response.Data.JWToken;
    }
}

public interface IHouseClient {
    Task<Response<AuthenticationResponse>> LoginAsync(AuthenticationRequest request);
    Task<Response<AuthenticationResponse>> RegisterAsync(RegisterRequest request);
    Task<Response<List<string>>> GetHousesAsync();

    Task<Response<List<Sensor>>> PostSensorAsync(Sensor sensor);
}

