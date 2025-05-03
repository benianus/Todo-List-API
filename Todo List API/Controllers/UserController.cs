using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoListBusinessLayer;
using ToDoListDataLayer;
using ToDoListDataLayer.Dtos;

namespace Todo_List_API.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost("singup")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDto>> SingUp([FromBody] UserDto NewUser)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        int userId = await UserBusiness.SingUp(NewUser);

        if (userId <= 0)
        {
            return BadRequest("Failed To Create");
        }

        NewUser.UserId = userId;

        return Ok(NewUser);
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Login([FromBody] LoginDto user)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userFounded = await UserBusiness.Login(user);  

        if (userFounded == null)
        {
            return Unauthorized("Unauthorized user");
        }

        return Ok(userFounded);
    }
}
