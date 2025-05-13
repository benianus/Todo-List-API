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
    [DataType(DataType.Text)]
    public string Name { get; set; }
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
