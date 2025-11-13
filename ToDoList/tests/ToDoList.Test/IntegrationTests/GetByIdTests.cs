namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Models;
using ToDoList.WebApi;
using ToDoList.Persistence;

public class GetByIdTests
{
    [Fact]
    public void GetById_ValidId_ReturnsItem()
    {
        // Arrange
        var connectionString = "Data Source=../../../IntegrationTests/data/localdb_test.db";
        using var context = new ToDoItemsContext(connectionString); //proč se používá using v rámci definování nové proměnné???
        var controller = new ToDoItemsController(repository: null); //tady se používá nějaká repository magic

        var toDoItem = new ToDoItem
        {
            //ToDoItemId = 1, - již nepoužíváme ID, protože databáze se o svoje ID stará sama
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };
        context.ToDoItems.Add(toDoItem);
        context.SaveChanges();

        // Act
        var result = controller.ReadById(toDoItem.ToDoItemId); //zde to zůstává stejné, protože změna na práci s DB proběhla již v rámci metody v controlleru
        var resultResult = result.Result;
        var value = result.GetValue();

        // Assert
        Assert.IsType<OkObjectResult>(resultResult);
        Assert.NotNull(value);

        Assert.Equal(toDoItem.ToDoItemId, value.toDoItemId);  //zůstává stejné
        Assert.Equal(toDoItem.Description, value.description);
        Assert.Equal(toDoItem.IsCompleted, value.isCompleted);
        Assert.Equal(toDoItem.Name, value.name);
    }

    [Fact]
    public void GetById_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var connectionString = "Data Source=../../../IntegrationTests/data/localdb_test.db";
        using var context = new ToDoItemsContext(connectionString);
        var controller = new ToDoItemsController(repository: null);

        //var toDoItem = new ToDoItem{ToDoItemId = 1,Name = "Jmeno",Description = "Popis",IsCompleted = false};
        //controller.items.Add(toDoItem);
        //POZN - teď už nevkládáme do databáze objekt, ale jen zkoušíme neexistující ID

        // Act
        var invalidId = -1;
        var result = controller.ReadById(invalidId);
        var resultResult = result.Result;

        // Assert
        Assert.IsType<NotFoundResult>(resultResult);
        //zbytek zůstává stejný
    }
}
