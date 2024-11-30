using System.Text.Json.Serialization;
namespace TaskManagementApi.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Status
{
    Pending,
    InProgress,
    Completed,
}
