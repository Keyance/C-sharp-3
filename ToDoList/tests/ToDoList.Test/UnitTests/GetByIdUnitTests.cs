namespace ToDoList.Test.UnitTests;

using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi;
using ToDoList.Domain.DTOs;

public class GetByIdTests
{
    [Fact]
    public async Task Get_ReadByIdWhenSomeItemAvailable_ReturnsOk()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };
        repositoryMock.ReadByIdAsync(1).Returns(Task.FromResult<ToDoItem?>(toDoItem));

        // Act
        var result = await controller.ReadByIdAsync(1);
        var resultResult = result.Result;

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(resultResult);
        var value = okResult.Value as ToDoItemGetResponseDto;
        Assert.IsType<OkObjectResult>(resultResult);

        Assert.NotNull(value);
        Assert.Equal(toDoItem.ToDoItemId, value.toDoItemId);
        Assert.Equal(toDoItem.Name, value.name);
        Assert.Equal(toDoItem.Description, value.description);
        Assert.Equal(toDoItem.IsCompleted, value.isCompleted);

        repositoryMock.Received(1).ReadByIdAsync(1);
    }

    [Fact]
    public async Task Get_ReadByIdWhenItemIsNull_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        repositoryMock.ReadByIdAsync(Arg.Any<int>()).Returns((ToDoItem?)null);

        // Act
        var result = await controller.ReadByIdAsync(1);
        var resultResult = result.Result;

        // Assert
        Assert.IsType<NotFoundResult>(resultResult);

        repositoryMock.Received(1).ReadByIdAsync(1);
    }

    [Fact]
    public async Task Get_ReadByIdUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        repositoryMock.ReadByIdAsync(Arg.Any<int>()).Returns(x => Task.FromException<ToDoItem?>(new Exception("Database error")));
        var controller = new ToDoItemsController(repositoryMock);

        // Act
        var result = await controller.ReadByIdAsync(1);
        var resultResult = result.Result;

        // Assert
        Assert.IsType<ObjectResult>(resultResult);
        var objectResult = resultResult as ObjectResult;
        Assert.Equal(500, objectResult?.StatusCode);

        await repositoryMock.Received(1).ReadByIdAsync(1);
    }
}
