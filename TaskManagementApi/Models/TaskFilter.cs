using System.Text.Json.Serialization;
using Microsoft.VisualBasic;
namespace TaskManagementApi.Models;

public class TaskFilter
{
    public int PageNo { get; set; }
    public int PageSize { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public string? Status { get; set; }
    public DateTime? DueDate  { get; set; }
}
