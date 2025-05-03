using System.ComponentModel.DataAnnotations;

namespace ToDoListDataLayer.Dtos;

public class TaskDto
{
    public TaskDto(int id, string title, string description, int userId)
    {
        Id = id;
        Title = title;
        Description = description;
        UserId = userId;
    }

    [Required]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public int UserId { get; set; }
}
