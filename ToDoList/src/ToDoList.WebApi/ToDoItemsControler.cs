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
    public IActionResult Create(ToDoItemCreateRequestDto request)
    {
        //map to Domain object as soon as possible
        var item = request.ToDomain();

        //try to create an item
        try
        {

            item.ToDoItemId = items.Count == 0 ? 1 : items.Max(o => o.ToDoItemId) + 1;
            items.Add(item);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

        //respond to client
        return Created(); //201
    }







    [HttpGet]
    public IActionResult Read()
    {
        try
        {
            if (items.Count() == 0)
            {
                return BadRequest("There are no items in the database.");
            }
            else
            {
                //posílám 200
                return Ok(items);
            }
        }
        catch (Exception ex)
        {
            //poslat 500
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }







    [HttpGet("{ToDoItemID:int}")]
    public IActionResult ReadByID(int ToDoItemID)
    {
        try
        {        // Pomocí LINQ hledám - dá se použít i Find se stejnou syntaxí
            var item = items.FirstOrDefault(i => i.ToDoItemId == ToDoItemID);

            if (item == null)
            {
                // 400
                return BadRequest($"Item with ID {ToDoItemID} not found.");
            }

            // Pokud existuje - vracím 200 OK a ten jeden item
            return Ok(item);
        }
        catch (Exception ex)
        {
            //500
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }





    [HttpPut("{ToDoItemID:int}")]
    public IActionResult UpdateByID(int ToDoItemID, [FromBody] ToDoItemUpdateRequestDto request)
    {
        try
        {
            if (!isIdInItems(ToDoItemID))
            {
                return BadRequest($"Item with ID {ToDoItemID} not found."); // 400
            }

            int index = items.FindIndex(i => i.ToDoItemId == ToDoItemID);

            if (index == -1)
            {
                return BadRequest($"Item with ID {ToDoItemID} not found."); // bezpečnostní - není nutné zde mít, protože už kontroluji, zda je v databázi - je zde kdyby se v mezičase něco změnilo
            }

            var existingItem = items[index];

            existingItem.Name = request.Name;
            existingItem.Description = request.Description;
            existingItem.IsCompleted = request.IsCompleted;


            items[index] = existingItem;


            return NoContent();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }





    [HttpDelete("{ToDoItemID:int}")]
    public IActionResult BeleteByID(int ToDoItemID)
    {
        try
        {
            // najdi položku podle ID
            var item = items.Find(i => i.ToDoItemId == ToDoItemID);

            if (item == null)
            {
                return NotFound($"Item with ID {ToDoItemID} not found.");
            }

            // smaž ji přímo podle reference
            items.Remove(item);

            return NoContent(); // 204
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }


    private bool isIdInItems(int Id)
    {
        return items.Any(i => i.ToDoItemId == Id);
        //použito jednou
    }
}
