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
    public void Delete_ValidId_ReturnsNoContent()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        var toDoItem = new ToDoItem
        {
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };

        // Act
        var result = controller.DeleteById(toDoItem.ToDoItemId);


        //Assert
        Assert.IsType<NoContentResult>(result); //zjišťuje, zda v var result už není žádný obsah
    }

    [Fact]
    public void Delete_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        // Act
        var invalidId = -1;
        var result = controller.DeleteById(invalidId);

        // Assert
        Assert.IsType<NotFoundResult>(result); //kontroluje že výsledek metody DeleteById je not found (404)
    }

    //BRAK-OUT ROOM testy
    [Fact]
    public void Delete_ValidItemId_ReturnsNoContent()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.GetById(Arg.Any<int>()).Returns(
new ToDoItem { Name = "test", Description = "test", IsCompleted = false }
        );
        var id = 1;

        //Act
        var result = controller.DeleteById(id);

        //Assert
        Assert.IsType<NoContentResult>(result);
        repositoryMock.Received(1).GetById(id);
        repositoryMock.Received(1).Delete(id);
    }

    [Fact]
    public void Delete_AnyItemIdExceptionOccurredDuringReadById_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        repositoryMock.GetById(1).Throws(new Exception("au"));
        var controller = new ToDoItemsController(repositoryMock);

        // Act
        var result = controller.DeleteById(1);

        // Assert
        var error = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, error.StatusCode);
        repositoryMock.Received(1).GetById(1);
    }
    [Fact]
    public void Delete_AnyItemIdExceptionOccurredDuringDeleteById_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        repositoryMock.GetById(1).Returns(new ToDoItem());
        repositoryMock
                .When(x => x.Delete(Arg.Any<int>()))
                .Do(_ => throw new Exception());
        var controller = new ToDoItemsController(repositoryMock);

        // Act
        var result = controller.DeleteById(1);

        // Assert
        var error = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, error.StatusCode);
        repositoryMock.Received(1).GetById(1);
        repositoryMock.Received(1).Delete(1);

    }
}
