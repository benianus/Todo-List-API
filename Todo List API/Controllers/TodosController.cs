using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ToDoListBusinessLayer;
using ToDoListDataLayer.Dtos;

namespace Todo_List_API.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTasks([FromQuery] int page, [FromQuery] int limite)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TodosDto tasks = await TodoBusiness.GetAllTasks(page, limite);

            if (tasks == null)
            {
                return NotFound("Tasks Not found");
            }

            return Ok(tasks);
        }
        [HttpGet("{id}", Name = "gettaskbyid")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTask([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var task = await TodoBusiness.GetTask(id);

            if (task == null)
            {
                return NotFound("Task not found");
            }

            return Ok(task);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTask([FromBody] TaskDto newTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int createTaskId = await TodoBusiness.CreateTask(newTask);

            newTask.Id = createTaskId;

            return CreatedAtRoute("gettaskbyid", new { id = newTask.Id }, newTask);
        }
        [HttpPut("{id}", Name = "updateTask")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTask([FromRoute] int id, [FromBody][Bind(["Title", "Description"])] TaskDto updatedTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = await TodoBusiness.UpdateTask(id, updatedTask);

            if (!isUpdated)
            {
                return BadRequest("Failed to update or Task Not found");
            }

            return Ok("Task Updated Successfully");
        }
        [HttpDelete("{id}", Name = "deleteTask")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTask([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            var isDeleted = await TodoBusiness.DeleteTask(id);

            if (!isDeleted)
            {
                return BadRequest("Failed to Delete or Task Not found");
            }

            return Ok("Task Deleted Successfully");
        }
    }
}
