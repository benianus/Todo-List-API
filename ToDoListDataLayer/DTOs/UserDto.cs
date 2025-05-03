using System.ComponentModel.DataAnnotations;

namespace ToDoListDataLayer.Dtos;

public class UserDto
{
    public UserDto(int userId, string name, string email, string password)
    {
        UserId = userId;
        Name = name;
        Email = email;
        Password = password;
    }

    [Required]
    public int UserId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
