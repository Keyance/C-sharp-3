namespace ToDoList.Frontend.Models;

using System.ComponentModel.DataAnnotations;

public class ToDoItemView()
{

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }

    public string? Kategory { get; set; }
}
