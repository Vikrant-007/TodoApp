using System.Net;

namespace TodoApp.Shared.Responses;

public record Response(bool Status = false, string Message = default!, int StatusCode = (int)HttpStatusCode.NoContent);
