namespace ToDoList.Frontend.Clients;

using ToDoList.Domain.DTOs;
using ToDoList.Frontend.Models;

public class ToDoItemsClient : IToDoItemsClient
{
    private readonly HttpClient httpClient;

    public ToDoItemsClient(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<List<ToDoItemView>> ReadItemsAsync()
    {
        var toDoItemViews = new List<ToDoItemView>();
        var response = await httpClient.GetFromJsonAsync<List<ToDoItemGetResponseDto>>("api/ToDoItems");

        toDoItemViews = response.Select(dto => new ToDoItemView()
        {
            Id = dto.toDoItemId,
            Name = dto.name,
            Description = dto.description,
            IsCompleted = dto.isCompleted,
            Kategory = dto.Kategory
        }).ToList();

        return toDoItemViews;
    }
    public async Task<ToDoItemView?> ReadItemByIdAsync(int itemId)
    {
        var response = await httpClient.GetFromJsonAsync<ToDoItemGetResponseDto>($"api/ToDoItems/{itemId}");

        var toDoItem = new ToDoItemView()
        {
            Id = response.toDoItemId,
            Name = response.name,
            Description = response.description,
            IsCompleted = response.isCompleted,
            Kategory = response.Kategory
        };
        return toDoItem;
    }

    public async Task UpdateItemAsync(ToDoItemView item)
    {
        // try {}
        var itemRequest = new ToDoItemUpdateRequestDto(item.Name, item.Description, item.IsCompleted, item.Kategory);
        var response = await httpClient.PutAsJsonAsync($"api/ToDoItems/{item.Id}", itemRequest);
    }
    public async Task DeleteItemAsync(int itemId)
    {
        await httpClient.DeleteAsync($"api/ToDoItems/{itemId}");
    }

    public async Task CreateItemAsync(ToDoItemView item)
    {
        var request = new ToDoItemCreateRequestDto(item.Name, item.Description, item.IsCompleted, item.Kategory);
        await httpClient.PostAsJsonAsync("api/ToDoItems", request);
    }
}
