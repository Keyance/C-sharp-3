namespace ToDoList.WebApi;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;

[Route("api/[controller]")] //localhost:5000/api/ToDoItems
[ApiController]
public class ToDoItemsController : ControllerBase
{
    private static List<ToDoItem> items = [];

    [HttpPost]
    public IActionResult Create(ToDoItemCreateRequestDTO request)
    {
        return Ok();
    }
    [HttpGet]
    public IActionResult Read()
    {
        return Ok();
    }
    [HttpGet("{ToDoItemID:int}")]
    public IActionResult ReadByID(int ToDoItemID)
    {
        return Ok();
    }
   [HttpGet("{toDoItemId:int}")]
    public IActionResult ReadById(int toDoItemId) //api/ToDoItems/<id> GET
    {
        try
        {
            throw new Exception("Neco se opravdu nepovedlo.");
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }
        return Ok(); //200
    }

    [HttpDelete("{ToDoItemID:int}")]
    public IActionResult BeleteByID(int ToDoItemID)
    {
        return Ok();
    }
}
