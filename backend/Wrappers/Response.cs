namespace backend.Wrappers;

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