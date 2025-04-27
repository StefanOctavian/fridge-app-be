using System.Net;
using System.Text.Json.Serialization;

namespace BussinessLogic.Errors;

public record ServiceError(string Message);
