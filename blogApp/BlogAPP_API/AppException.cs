public abstract class AppException : Exception
{
    public int StatusCode { get; }
    public string Code { get; }

    protected AppException(int statusCode, string code, string message) : base(message)
    {
        StatusCode = statusCode;
        Code = code;
    }
}

public sealed class NotFoundAppException : AppException
{
    public NotFoundAppException(string message = "Not found")
        : base(404, "not_found", message) { }
}

public sealed class BadRequestAppException : AppException
{
    public BadRequestAppException(string message = "Bad request")
        : base(400, "bad_request", message) { }
}
