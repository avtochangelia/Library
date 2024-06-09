#nullable disable

using System.Net;

namespace Application.Shared.Exceptions;

[Serializable]
public class ApiException : Exception
{
    public ApiException(string message) : base(message)
    {
        HttpStatusCode = HttpStatusCode.InternalServerError;
        ErrorMessages.Add(message);
    }

    public ApiException(HttpStatusCode httpStatusCode, string message) : base(message)
    {
        HttpStatusCode = httpStatusCode;
        ErrorMessages.Add(message);
    }

    public HttpStatusCode HttpStatusCode { get; set; }

    public IList<string> ErrorMessages { get; set; } = new List<string>();
}