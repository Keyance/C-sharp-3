using System;

namespace ToDoList.Domain.DTOs;

public record ToDoItemRequestDto (string Name, string Description, bool IsCompleted)
{

}
