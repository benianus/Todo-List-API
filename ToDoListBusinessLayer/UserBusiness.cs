using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListDataLayer.Dtos;
using ToDoListDataLayer;

namespace ToDoListBusinessLayer;

public class UserBusiness
{
    public UserBusiness(UserDto user)
    {
        UserId = user.UserId;
        Name = user.Name;
        Email = user.Email;
        Password = user.Password;
    }
    public UserBusiness()
    {
        UserId = 0;
        Name = string.Empty;
        Email = string.Empty;
        Password = string.Empty;
    }
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    public static async Task<int> SingUp(UserDto NewUser)
    {
        return await UserData.SingUp(NewUser);
    }
    public static async Task<LoginDto> Login(LoginDto user)
    {
        return await UserData.Login(user);
    }
}
