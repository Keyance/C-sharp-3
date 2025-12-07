namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.WebApi;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;

public class PutTests
{
    [Fact]
    public async Task Put_ValidId_ReturnsNoContent()
    {
        // Arrange
        var connectionString = "Data Source=../../../IntegrationTests/data/localdb_test.db";
        using var context = new ToDoItemsContext(connectionString);
        var repository = new ToDoItemsRepository(context);
        var controller = new ToDoItemsController(repository);

        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };

        await context.ToDoItems.AddAsync(toDoItem);
        await context.SaveChangesAsync();

        var request = new ToDoItemUpdateRequestDto(
            Name: "Jine jmeno",
            Description: "Jiny popis",
            IsCompleted: true
        );

        // Act
        var result = await controller.UpdateById(toDoItem.ToDoItemId, request); //stejné, změna je již v Controlleru

        // Assert
        Assert.IsType<NoContentResult>(result);

        // Cleanup
        context.ToDoItems.Remove(toDoItem);
        await context.SaveChangesAsync();
    }

    [Fact]
    public async Task Put_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var connectionString = "Data Source=../../../IntegrationTests/data/localdb_test.db";
        using var context = new ToDoItemsContext(connectionString);
        var repository = new ToDoItemsRepository(context);
        var controller = new ToDoItemsController(repository);

        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };
        await context.ToDoItems.AddAsync(toDoItem);
        await context.SaveChangesAsync();

        var request = new ToDoItemUpdateRequestDto(
            Name: "Jine jmeno",
            Description: "Jiny popis",
            IsCompleted: true
        );

        // Act
        var invalidId = -1;
        var result = await controller.UpdateById(invalidId, request);

        // Assert
        Assert.IsType<NotFoundResult>(result);

        // Cleanup
        context.ToDoItems.Remove(toDoItem); //možná se to dá zabalit do if not null
        await context.SaveChangesAsync();
    }
}
