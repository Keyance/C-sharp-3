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
    [HttpPut("{ToDoItemID:int}")]
    public IActionResult UpdateByID(int ToDoItemID, [FromBody] ToDoItemRequestDto request)
    {
        try
        {
            throw new Exception("Něco se fakt nepovedlo");
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
        return Ok();
    }
    [HttpDelete("{ToDoItemID:int}")]
    public IActionResult BeleteByID(int ToDoItemID)
    {
        return Ok();
    }
}
