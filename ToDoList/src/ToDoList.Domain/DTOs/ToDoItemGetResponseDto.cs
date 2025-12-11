using System;

namespace ToDoList.Domain.DTOs;

using System.Collections.Generic;
using ToDoList.Domain.Models;

public record ToDoItemGetResponseDto(int toDoItemId, string name, string description, bool isCompleted, string? Kategory)
{
    //public IEnumerable<object>? Id { get; set; }

    public static ToDoItemGetResponseDto FromDomain(ToDoItem item) => new(item.ToDoItemId, item.Name, item.Description, item.IsCompleted, item.Kategory);

}
