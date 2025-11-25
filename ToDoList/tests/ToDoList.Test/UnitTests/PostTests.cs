namespace ToDoList.Test.UnitTests;

using NSubstitute;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.WebApi;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;
using ToDoList.Domain.Models;
using ToDoList.Test;

public class PostTests
{
    [Fact]
    public void Post_ValidRequest_ReturnsNewItem()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();

        var controller = new ToDoItemsController(repositoryMock); //toto je zatím prasárna, protože na context už pak nesaháme

        var request = new ToDoItemCreateRequestDto(
            Name: "Jmeno",
            Description: "Popis",
            IsCompleted: false
        );

        // Act
        var result = controller.Create(request); //zase zůstává stejné
        var resultResult = result.Result;
        var value = result.GetValue();

        // Assert
        Assert.IsType<CreatedAtActionResult>(resultResult);
        Assert.NotNull(value);

        Assert.Equal(request.Description, value.description);
        Assert.Equal(request.IsCompleted, value.isCompleted);
        Assert.Equal(request.Name, value.name);
    }

    [Fact]
    public void Post_CreateUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();

        var controller = new ToDoItemsController(repositoryMock); //toto je zatím prasárna, protože na context už pak nesaháme

        var request = new ToDoItemCreateRequestDto(
            Name: "Jmeno",
            Description: "Popis",
            IsCompleted: false
        );
        repositoryMock.When(x => x.Create(Arg.Any<ToDoItem>()))
                      .Do(_ => throw new Exception());
        // Act
        var result = controller.Create(request);
        var resultResult = result.Result;
        var value = result.GetValue();

        // Assert
        var error = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, error.StatusCode);

        repositoryMock.Received(1).Create(Arg.Any<ToDoItem>());
    }
}
