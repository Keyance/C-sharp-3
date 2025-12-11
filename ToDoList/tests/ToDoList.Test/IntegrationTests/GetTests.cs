namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Models;
using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.DTOs;
using ToDoList.WebApi;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;

/*
public class GetTests
{
    [Fact]
    public async Task Get_AllItems_ReturnsAllItems()
    {
        // Arrange
        var connectionString = "Data Source=../../../IntegrationTests/data/localdb_test.db";
        using var context = new ToDoItemsContext(connectionString);
        var repository = new ToDoItemsRepository(context);
        var controller = new ToDoItemsController(repository);

        var todoItem1 = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno1",
            Description = "Popis1",
            IsCompleted = false
        };
        var todoItem2 = new ToDoItem
        {
            ToDoItemId = 2,
            Name = "Jmeno2",
            Description = "Popis2",
            IsCompleted = true
        };
        //var controller = new ToDoItemsController(); - již máme nahoře
        //controller.AddItemToStorage(todoItem1); - stále by fungovalo minimálně zavolání metody, dokud ji nesmažu, až nebude mít reference

        await context.ToDoItems.AddAsync(todoItem1); //přidáváme pomocí EF Core metody Add
        await context.ToDoItems.AddAsync(todoItem2);
        await context.SaveChangesAsync();


        // Act
        var result = await controller.Read();
        var resultResult = result.Result;

        // Assert
         var okResult = Assert.IsType<OkObjectResult>(resultResult);
        var value = okResult.Value as IEnumerable<ToDoItemGetResponseDto>;
        Assert.NotNull(value);

        var firstToDo = value.First();
        Assert.Equal(todoItem1.ToDoItemId, firstToDo.toDoItemId);
        Assert.Equal(todoItem1.Name, firstToDo.name);
        Assert.Equal(todoItem1.Description, firstToDo.description);
        Assert.Equal(todoItem1.IsCompleted, firstToDo.isCompleted);

        // Cleanup
        context.ToDoItems.Remove(todoItem1);
        context.ToDoItems.Remove(todoItem2);
        await context.SaveChangesAsync();
    }
}
*/
