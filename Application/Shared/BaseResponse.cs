#nullable disable

using Newtonsoft.Json;
using System.Net;

namespace Application.Shared;

public class BaseResponse(string message, HttpStatusCode httpStatusCode)
{
    public HttpStatusCode HttpStatusCode { get; private set; } = httpStatusCode;

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string Message { get; set; } = message;

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public List<string> Errors { get; set; }
}