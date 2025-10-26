namespace ToDoList.Test;

using System.Reflection;
using ToDoList.Domain.Models;
using ToDoList.WebApi;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
public class UpdateTests
{
    [Fact]
    public void UpdateTest()
    {
        ToDoItemsController.items.Clear();

        //arrange
        var controller = new ToDoItemsController();
        ToDoItemsController.items.Add(new ToDoItem
        {
            ToDoItemId = 3,
            Name = "original",
            Description = "something is here",
            IsCompleted = false
        });

        var update = new ToDoItemUpdateRequestDto("updated", "updated desription", true);

        //act
        var result = controller.UpdateById(3, update);

        //assert
        Assert.IsType<NoContentResult>(result);

        var stored = ToDoItemsController.items.Single(i => i.ToDoItemId == 3);
        Assert.Equal(3, stored.ToDoItemId);
        Assert.Equal(update.Name, stored.Name);
        Assert.Equal(update.Description, stored.Description);
        Assert.Equal(update.IsCompleted, stored.IsCompleted);
    }
}
