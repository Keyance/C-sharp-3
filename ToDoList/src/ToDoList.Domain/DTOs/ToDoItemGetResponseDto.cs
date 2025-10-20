using System;

namespace ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;

public class ToDoItemGetResponseDto
{
    public ToDoItemGetResponseDto(int toDoItemId, string name, string description, bool isCompleted)
    {
    }

    public static ToDoItemGetResponseDto FromDomain(ToDoItem item) => new(item.ToDoItemId, item.Name, item.Description, item.IsCompleted);

}
