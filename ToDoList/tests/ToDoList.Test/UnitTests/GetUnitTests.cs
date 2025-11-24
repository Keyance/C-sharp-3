using System;
using System.Collections.Generic;
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
        public void Get_ReadWhenSomeItemAvailable_ReturnsOk()
        {
            //Arrange
            var repositoryMock = Substitute.For<IRepository<ToDoItem>>();

            var controller = new ToDoItemsController(repositoryMock);

            var someItem = new ToDoItem { Name = "testname", Description = "test", IsCompleted = false }; //konfigurace mocku, aby vracel toto
            repositoryMock.GetAll().Returns([someItem]); //tady říkáme, co má vracet

            //Act
            var result = controller.Read();
            var resultResult = result.Result;

            //Assert
            Assert.IsType<IEnumerable<ToDoItemGetResponseDto>>(result);
            repositoryMock.Received(1).GetAll();
        }

        [Fact]
        public void Get_ReadWhenNoItemAvailable_ReturnsNotFound()
        {
            //Arrange
            var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
            var controller = new ToDoItemsController(repositoryMock);
            repositoryMock.GetAll().Returns(new List<ToDoItem>()); //prázdná kolekce

            //Act
            var actionResult = controller.Read();
            var result = actionResult.Result;

            //Assert
            Assert.IsType<NotFoundResult>(result);
            repositoryMock.Received(1).GetAll();

        }

        [Fact]
        public void Get_ReadUnhandledException_ReturnsInternalServerError()
        {
            // Arrange
            var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
            var controller = new ToDoItemsController(repositoryMock);
            repositoryMock.When(x => x.Create(Arg.Any<ToDoItem>()))
                          .Do(_ => throw new Exception());
            // Act
            var result = controller.Read().Result;

            // Assert
            var error = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, error.StatusCode);

            repositoryMock.Received(1).GetAll();
        }
    }
}
