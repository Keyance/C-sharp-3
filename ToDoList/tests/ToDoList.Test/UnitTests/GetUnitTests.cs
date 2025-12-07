using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi;

namespace ToDoList.Test.UnitTests
{
    public class GetUnitTests
    {
        [Fact]
        public async Task Get_ReadWhenSomeItemAvailable_ReturnsOk()
        {
            //Arrange
            var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();

            var controller = new ToDoItemsController(repositoryMock);

            var someItem = new ToDoItem
            {
                ToDoItemId = 1,
                Name = "testName",
                Description = "testDescription",
                IsCompleted = false
            };
            var someItemList = new List<ToDoItem> { someItem };
            repositoryMock.ReadAllAsync().Returns(Task.FromResult<IEnumerable<ToDoItem>>(someItemList));

            //Act
            var result = await controller.Read();
            var resultResult = result.Result;

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(resultResult);
            var value = okResult.Value as IEnumerable<ToDoItemGetResponseDto>;
            Assert.NotNull(value);
            Assert.Single(value);
            Assert.Equal(someItem.ToDoItemId, value.First().toDoItemId);
            Assert.Equal(someItem.Name, value.First().name);

            await repositoryMock.Received(1).ReadAllAsync();
        }

        [Fact]
        public async Task Get_ReadWhenNoItemAvailable_ReturnsNotFound()
        {
            // Arrange
            var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
            var controller = new ToDoItemsController(repositoryMock);

            repositoryMock.ReadAllAsync().Returns(Task.FromResult<IEnumerable<ToDoItem>>(null!));

            // Act
            var result = await controller.Read();
            var resultResult = result.Result;

            // Assert
            Assert.IsType<NotFoundResult>(resultResult);

            await repositoryMock.Received(1).ReadAllAsync();
        }

        [Fact]
        public async Task Get_ReadUnhandledException_ReturnsInternalServerError()
        {
            // Arrange
            var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
            repositoryMock.ReadAllAsync().Returns(x => Task.FromException<IEnumerable<ToDoItem>>(new Exception("Database error")));
            var controller = new ToDoItemsController(repositoryMock);

            // Act
            var result = await controller.Read();
            var resultResult = result.Result;

            // Assert
            Assert.IsType<ObjectResult>(resultResult);
            var objectResult = resultResult as ObjectResult;
            Assert.Equal(500, objectResult?.StatusCode);

            await repositoryMock.Received(1).ReadAllAsync();
        }
    }
}
