using System;

namespace ToDoList.Test;

using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain;
using ToDoList.Domain.Models;
using ToDoList.WebApi;

public class GetTests
{
    [Fact]
    public void Get_AllItems_ReturnsAllItems()
    {
        //arrange
        var toDoItem1 = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };

        var toDoItem2 = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };

        var controler = new ToDoItemsController();
        controler.AddItemtoStorage(toDoItem1);
        controler.AddItemtoStorage(toDoItem1);

        //act
        var result = controler.Read();
        var value = result.GetValue();

        // Assert
        Assert.NotNull(value);

        var firstToDo = value.First();
        Assert.Equal(toDoItem1.ToDoItemId, firstToDo.Id);
        Assert.Equal(toDoItem1.Name, firstToDo.Name);
        Assert.Equal(toDoItem1.Description, firstToDo.Description);
        Assert.Equal(toDoItem1.IsCompleted, firstToDo.IsCompleted);

    }

    [Fact]
    public void DeleteTest()
    {
        // Arrange

        //zadefinování controleru a 

        // Act

        // Then
    }
}
