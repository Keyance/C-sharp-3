namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Models;
using ToDoList.WebApi;
using ToDoList.Persistence;

public class DeleteTests
{
    [Fact]
    public void Delete_ValidId_ReturnsNoContent()
    {
        // Arrange
        var connectionString = "Data Source=../../../IntegrationTests/data/localdb_test.db"; //vytváříme si jinou db jenom pro testy
        using var context = new ToDoItemsContext(connectionString); //funguje v rámci závorek odsud po konec metody
        var controller = new ToDoItemsController(context: context, repository: null);

        var toDoItem = new ToDoItem
        {
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };
        context.ToDoItems.Add(toDoItem);
        context.SaveChanges();

        // Act
        var result = controller.DeleteById(toDoItem.ToDoItemId);


        //Assert
        Assert.IsType<NoContentResult>(result); //zjišťuje, zda v var result už není žádný obsah

        // Verify item was deleted
        var deletedItem = context.ToDoItems.Find(toDoItem.ToDoItemId); //snažím se do proměné vložit item, který jsem mazala
        Assert.Null(deletedItem); //ověřuji, že se mi v předchozím řádku žádný item nenahrál
    }

    [Fact]
    public void Delete_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var connectionString = "Data Source=../../../IntegrationTests/data/localdb_test.db";
        using var context = new ToDoItemsContext(connectionString);
        var controller = new ToDoItemsController(context: context, repository: null);

        // Act
        var invalidId = -1;
        var result = controller.DeleteById(invalidId);

        // Assert
        Assert.IsType<NotFoundResult>(result); //kontroluje že výsledek metody DeleteById je not found (404)
    }
}
