namespace ToDoList.Test;

using System.Reflection;
using ToDoList.Domain.Models;
using ToDoList.WebApi;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
public class DeleteTests
{
    [Fact]
    public void DeleteTest()
    {
        ToDoItemsController.items.Clear();
        // Arrange
        var controller = new ToDoItemsController();
        ToDoItemsController.items.Add(new ToDoItem
        {
            ToDoItemId = 1,
            Name = "DeletedItem",
            Description = "delete",
            IsCompleted = true
        });

        //zadefinování controleru a


        // Act
        var result = controller.DeleteById(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
        Assert.DoesNotContain(ToDoItemsController.items, i => i.ToDoItemId == 1);
    }

}
