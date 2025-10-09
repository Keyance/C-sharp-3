using System;

namespace ToDoList.Domain.DTOs
{
    public record ToDoItemCreateRequestDTO (string Name, string Description, bool IsCompleted)
    {

    }
}
