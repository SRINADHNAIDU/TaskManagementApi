using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TaskManagementApi.Controllers;
using TaskManagementApi.Models;
using TaskManagementApi.Services;
namespace TaskManagementApi.TestCases;

[TestFixture]
public class TaskControllerTest
{
    private TaskController _controller;
    private Mock<ITaskService> _taskServiceMock;
    private Mock<ILogger<TaskController>> _loggerMock;

    [SetUp]
    public void Setup()
    {
        _taskServiceMock = new Mock<ITaskService>();
        _taskServiceMock = new Mock<ITaskService>();
        _loggerMock = new Mock<ILogger<TaskController>>();
        _controller = new TaskController(_loggerMock.Object, _taskServiceMock.Object);
    }
    
    [Test]
    public async Task GetFilteredProducts_ReturnsOk_WithProducts()
    {
        // Arrange
        var filter = new TaskFilter() { PageNo = 1, PageSize = 10 };
        var mockData = new List<Entities.Task>
        {
            new Entities.Task { Id = 1, Description = "Task1", Title = "mailTask1" , DueDate = DateTime.Now, },
            new Entities.Task { Id = 2, Description = "Task1", Title = "mailTask2" , DueDate = DateTime.Now, },
        };
        PagedResponse<Entities.Task> mockedResponse = new PagedResponse<Entities.Task>();
        mockedResponse.Data = mockData;
        mockedResponse.Count = mockData.Count;
        
        _taskServiceMock.Setup(service => service.GetAllTasks(filter))
            .ReturnsAsync(mockedResponse);

        // Act
        var result = await _controller.GetAllTasks(filter);
        
            
    }
}
