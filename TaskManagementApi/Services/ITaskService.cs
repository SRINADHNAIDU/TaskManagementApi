using TaskManagementApi.Models;
namespace TaskManagementApi.Services;

public interface ITaskService
{
    Task<PagedResponse<TaskManagementApi.Entities.Task>> GetAllTasks(TaskFilter? filter);
    Task<TaskManagementApi.Entities.Task?> GetTaskById(int id);
    Task<TaskManagementApi.Entities.Task> CreateTask(TaskManagementApi.Entities.Task task);
    Task<TaskManagementApi.Entities.Task> UpdateTask(TaskManagementApi.Entities.Task task);
    Task DeleteTask(int id);
}
