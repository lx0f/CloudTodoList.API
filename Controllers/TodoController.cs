using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using CloudTodoList.API.Models;
using CloudTodoList.API.Services;

namespace CloudTodoList.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private readonly ITodoService _service;

    public TodoController(ITodoService service)
    {
        _service = service;
    }

    [EnableCors]
    [HttpGet]
    public async Task<IActionResult> GetAllTodos()
    {
        IEnumerable<Todo> todos = await _service.GetAll();

        return Ok(todos);
    }

    [EnableCors]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTodoById(int id)
    {
        Todo? todo = await _service.GetById(id);

        if (todo == null)
            return NotFound();

        return Ok(todo);
    }

    [EnableCors]
    [HttpPost]
    public async Task<IActionResult> CreateTodo(TodoDto todoDto)
    {
        bool success = await _service.Create(todoDto);

        if (!success)
            return Conflict();

        return NoContent();
    }

    [EnableCors]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodo(int id, TodoDto todoDto)
    {
        bool success = await _service.Update(id, todoDto);

        if (!success)
            return NotFound();

        return NoContent();
    }

    [EnableCors]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo(int id)
    {
        bool success = await _service.Delete(id);

        if (!success)
            return NotFound();

        return NoContent();
    }
}
