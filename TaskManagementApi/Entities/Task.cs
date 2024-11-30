using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskManagementApi.Enums;
namespace TaskManagementApi.Entities;

public class Task
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public Status Status { get; set; }
    public required DateTime DueDate { get; set; }
}
