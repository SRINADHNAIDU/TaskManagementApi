using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Models;
using TaskManagementApi.Services;

namespace TaskManagementApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController(ILogger<TaskController> _logger, ITaskService _service) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAllTasks([FromQuery] TaskFilter? filter)
    {
        try
        {
            var pagedResponse = await _service.GetAllTasks(filter);
            if (!pagedResponse.Data.Any())
            {
                _logger.LogInformation("No tasks found");
                return NotFound("No tasks found");
            }
            _logger.LogInformation("Found {count} tasks", pagedResponse.Count);
            return Ok(pagedResponse);
        }
        catch (Exception e)
        {
            _logger.LogError("Error Occured While Retrieving Tasks", e);
            return StatusCode(500, e.Message);
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaskById(int id)
    {
        try
        {

            var task = await _service.GetTaskById(id);
            if (task == null)
            {
                _logger.LogInformation("No task found with id {id}", id);
                return NotFound($"No task found with id {id}");
            }
            _logger.LogInformation("Found {id} task", id);
            return Ok(task);
        }
        catch (Exception e)
        {
            _logger.LogError("Error Occured While Retrieving Task", e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask(TaskManagementApi.Entities.Task task)
    {
        try
        {
            await _service.CreateTask(task);
            _logger.LogInformation("Created task {taskId}", task.Id);
            return Ok(task);
        }
        catch (Exception e)
        {
            _logger.LogError("Error Occured While Creating Task", e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTask(TaskManagementApi.Entities.Task task)
    {
        try
        {
            await _service.UpdateTask(task);
            _logger.LogInformation("Updated task {taskId}", task.Id);
            return Ok(task);
        }
        catch (Exception e)
        {
            _logger.LogError("Error Occured While Updating Task", e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        try
        {
            await _service.DeleteTask(id);
            _logger.LogInformation("deleted task {id}", id);
            return Ok("Deleted task");
        }
        catch (Exception e)
        {
            _logger.LogError("Error Occured While Deleting Task", e);
            return StatusCode(500, e.Message);
        }
    }
}
