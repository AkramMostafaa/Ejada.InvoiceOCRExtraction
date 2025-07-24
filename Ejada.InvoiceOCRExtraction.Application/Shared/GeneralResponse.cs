using System.Net;

namespace Ejada.InvoiceOCRExtraction.Application.Shared;

public class GeneralResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    public List<string>? Errors { get; set; }

    public static GeneralResponse<T> SuccessResponse(T data, string message = "Success", HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new GeneralResponse<T>
        {
            Success = true,
            Message = message,
            Data = data,
            StatusCode = statusCode
        };
    }

    public static GeneralResponse<T> FailResponse(string message = "Failed", HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new GeneralResponse<T>
        {
            Success = false,
            Message = message,
            StatusCode = statusCode,
            Data = default
        };
    }
    public static GeneralResponse<T> FailResponse(string message, List<string>? errors = null, HttpStatusCode statusCode = HttpStatusCode.BadRequest) =>
        new() { Success = false, StatusCode = statusCode, Message = message, Errors = errors };
}
