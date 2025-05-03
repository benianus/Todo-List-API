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

    [Required(ErrorMessage = "Email Required")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required(ErrorMessage = "Password Required")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

}
