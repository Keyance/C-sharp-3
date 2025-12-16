namespace ToDoList.Test.UnitTests;

using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Models;
using ToDoList.WebApi;
using ToDoList.Persistence.Repositories;
public class DeleteTests
{
    [Fact]
    public async Task Delete_ValidId_ReturnsNoContent()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        var toDoItem = new ToDoItem
        {
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };
        repositoryMock.ReadByIdAsync(1).Returns(Task.FromResult<ToDoItem?>(toDoItem));

        // Act
        var result = await controller.DeleteById(1);


        //Assert
        Assert.IsType<NoContentResult>(result);

        await repositoryMock.Received(1).ReadByIdAsync(1);
        await repositoryMock.Received(1).DeleteByIdAsync(1);
    }

    [Fact]
    public async Task Delete_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        repositoryMock.ReadByIdAsync(1).Returns(Task.FromResult<ToDoItem?>(null));

        // Act
        var result = await controller.DeleteById(1);

        // Assert
        Assert.IsType<NotFoundResult>(result); //kontroluje že výsledek metody DeleteById je not found (404)

        await repositoryMock.Received(1).ReadByIdAsync(1);
        await repositoryMock.DidNotReceive().DeleteByIdAsync(Arg.Any<int>());
    }

    //BRAK-OUT ROOM testy
    [Fact]
    public async Task Delete_AnyItemIdExceptionOccurredDuringReadById_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        repositoryMock.ReadByIdAsync(1).Throws(new Exception("au"));
        var controller = new ToDoItemsController(repositoryMock);

        // Act
        var result = await controller.DeleteById(1);

        // Assert
        var error = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, error.StatusCode);
        await repositoryMock.Received(1).ReadByIdAsync(1);
    }
    [Fact]
    public async Task Delete_AnyItemIdExceptionOccurredDuringDeleteById_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        repositoryMock.ReadByIdAsync(1).Returns(new ToDoItem());
        repositoryMock
                .When(x => x.DeleteByIdAsync(Arg.Any<int>()))
                .Do(_ => throw new Exception());
        var controller = new ToDoItemsController(repositoryMock);

        // Act
        var result = await controller.DeleteById(1);

        // Assert
        var error = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, error.StatusCode);
        await repositoryMock.Received(1).ReadByIdAsync(1);
        await repositoryMock.Received(1).DeleteByIdAsync(1);

    }
}
