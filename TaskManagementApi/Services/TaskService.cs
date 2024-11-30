using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Enums;
using TaskManagementApi.Models;
namespace TaskManagementApi.Services;

public class TaskService(ILogger<TaskService> _logger, TaskDbContext _dbContext) : ITaskService
{
    public async Task<PagedResponse<TaskManagementApi.Entities.Task>> GetAllTasks(TaskFilter? filter)
    {
        IQueryable<Entities.Task> query = _dbContext.Tasks.AsQueryable();
        PagedResponse<TaskManagementApi.Entities.Task> pagedResponse = new();

        if (filter is null)
        {
            pagedResponse.Data = await query.ToListAsync();
            pagedResponse.Count = await query.CountAsync();
            return pagedResponse;
        }
        if (!string.IsNullOrEmpty(filter.Status))
        {
            query = query.Where(e => e.Status == (Status)Enum.Parse(typeof(Status), filter.Status));
        }

        if (filter.DueDate is not null)
        {
            query = query.Where(e => e.DueDate >= filter.DueDate);
        }
        pagedResponse.Count = query.Count();
        _logger.LogInformation("Tasks Count: {pagedResponse.Count}", pagedResponse.Count);
        pagedResponse.Data = filter.PageSize > 0
            ? await query
                .Skip((filter.PageNo - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync()
            : await query.ToListAsync();
        _logger.LogInformation("Returning paged tasks");
        return pagedResponse;
    }
    public async Task<TaskManagementApi.Entities.Task?> GetTaskById(int id)
    {
        var tasks = await _dbContext.Tasks.FirstOrDefaultAsync(e => e.Id == id);
        if (tasks == null)
        {
            _logger.LogInformation("Task with id {id} not found", id);
            return null;
        }
        _logger.LogInformation($"Task with id: {id} was found");
        return tasks;
    }

    public async Task<TaskManagementApi.Entities.Task> CreateTask(TaskManagementApi.Entities.Task task)
    {
        _dbContext.Tasks.Add(task);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"Task with id: {task.Id} was created");
        return task;
    }
    public async Task<TaskManagementApi.Entities.Task> UpdateTask(TaskManagementApi.Entities.Task task)
    {
        _dbContext.Tasks.Update(task);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"Task with id: {task.Id} was updated");
        return task;
    }
    public async Task DeleteTask(int id)
    {
        var task = await _dbContext.Tasks.FirstOrDefaultAsync(e => e.Id == id);
        if (task is null)
        {
            _logger.LogInformation("Task with id: {id} not found", id);
            throw new InvalidOperationException("Task with id: " + id + " not found");
        }
        _dbContext.Tasks.Remove(task);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"Task with id: {id} was deleted");
    }
}
