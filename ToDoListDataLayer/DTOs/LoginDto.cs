using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListDataLayer.Dtos;

public class LoginDto
{
    public LoginDto(string email, string password)
    {
        Email = email;
        Password = password;
    }

    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    
}
