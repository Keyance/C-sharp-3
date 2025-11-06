namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.WebApi;
using ToDoList.Persistence;

public class PostTests
{
    [Fact]
    public void Post_ValidRequest_ReturnsNewItem()
    {
        // Arrange
        //var controller = new ToDoItemsController();
        var connectionString = "Data Source=../../../IntegrationTests/data/localdb_test.db";
        using var context = new ToDoItemsContext(connectionString);
        var controller = new ToDoItemsController(context: context, repository: null);

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

        // Cleanup
        var createdItem = context.ToDoItems.Find(value.Id); //musíme najít jaké ID mu databáze přiřadila
        if (createdItem != null) //jenom pokud něco bylo vytvořeno
        {
            context.ToDoItems.Remove(createdItem);
            context.SaveChanges();
        }
    }
}
