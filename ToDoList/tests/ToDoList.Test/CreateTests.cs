namespace ToDoList.Test;

using System.Reflection;
using ToDoList.Domain.Models;
using ToDoList.WebApi;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
public class CreateTests
{
    [Fact]
    public void CreateTest()
    {
        ToDoItemsController.items.Clear(); //vymazávám si na začátku list, abych mohla pracovat v testu s čistým

        //arrange
        var controller = new ToDoItemsController();
        var request = new ToDoItemCreateRequestDto("addItem", "addDesc", true);

        //act
        var actionResult = controller.Create(request);
        var result = actionResult.Result as CreatedAtActionResult;

        //assert
        Assert.NotNull(result);
        Assert.Equal(nameof(ToDoItemsController.ReadById), result.ActionName);

        var dto = result.Value as ToDoItemGetResponseDto;
        Assert.NotNull(dto);
        Assert.Equal(request.Name, dto.name);
        Assert.Equal(request.Description, dto.description);
        Assert.Equal(request.IsCompleted, dto.isCompleted);
        Assert.Equal(1, dto.toDoItemId);

        Assert.Single(ToDoItemsController.items);
        Assert.Equal(1, ToDoItemsController.items[0].ToDoItemId);
    }
}
