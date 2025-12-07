namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.WebApi;
using Microsoft.EntityFrameworkCore;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;

public class PostTests
{
    [Fact]
    public async Task Post_ValidRequest_ReturnsNewItem()
    {
        // Arrange
        //var controller = new ToDoItemsController();
        var connectionString = "Data Source=../../../IntegrationTests/data/localdb_test.db";
        using var context = new ToDoItemsContext(connectionString);
        var repository = new ToDoItemsRepository(context);
        var controller = new ToDoItemsController(repository);

        var request = new ToDoItemCreateRequestDto(
            Name: "Jmeno",
            Description: "Popis",
            IsCompleted: false
        );

        // Act
        var result = await controller.Create(request); //zase zůstává stejné
        var resultResult = result.Result;

        // Assert
        var createdAtResult = Assert.IsType<CreatedAtActionResult>(resultResult);
        var value = createdAtResult.Value as ToDoItemGetResponseDto;
        Assert.IsType<CreatedAtActionResult>(resultResult);
        Assert.NotNull(value);

        Assert.Equal(request.Description, value.description);
        Assert.Equal(request.IsCompleted, value.isCompleted);
        Assert.Equal(request.Name, value.name);

        // Cleanup
        var createdItem = await context.ToDoItems.FindAsync(value.toDoItemId); //musíme najít jaké ID mu databáze přiřadila
        if (createdItem != null) //jenom pokud něco bylo vytvořeno
        {
            context.ToDoItems.Remove(createdItem);
            await context.SaveChangesAsync();
        }
    }
}
