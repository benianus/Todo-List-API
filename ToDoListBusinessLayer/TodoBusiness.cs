using ToDoListDataLayer;
using ToDoListDataLayer.Dtos;

namespace ToDoListBusinessLayer;

public class TodoBusiness
{
    public int ToDoId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int UserId { get; set; }

    public TodoBusiness(TaskDto task)
    {
        ToDoId = task.Id;
        Title = task.Title;
        Description = task.Description;
        UserId = task.UserId;
    }

    public static async Task<TodosDto> GetAllTasks(int page, int limite)
    {
        return await TodosData.GetAllTasks(page, limite);
    }
    public static async Task<TaskDto> GetTask(int id)
    {
        return await TodosData.GetTask(id);  
    }
    public static async Task<int> CreateTask(TaskDto newTask)
    {
        return await TodosData.CreateTask(newTask);
    }
    public static async Task<bool> UpdateTask(int id, TaskDto UpdateTask)
    {
        return await TodosData.UpdateTask(id, UpdateTask);
    }
    public static async Task<bool> DeleteTask(int id)
    {
        return await TodosData.DeleteTask(id);
    }

    public static async Task<TodosDto> FilterTasks(string filter, int page, int limite)
    {
        return await TodosData.FilterTasks(filter, page, limite);
    }
}
