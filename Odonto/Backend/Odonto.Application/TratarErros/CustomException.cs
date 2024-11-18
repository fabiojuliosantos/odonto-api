namespace Odonto.Application.TratarErros;

public class CustomException : Exception
{
    public CustomException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    public int StatusCode { get; }
}
