using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MyApi.Controllers;

[ApiController]
[EnableCors("_myAllowSpecificOrigins")]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private static readonly List<Todo> _data = new();

    private readonly ILogger<TodoController> _logger;

    public TodoController(ILogger<TodoController> logger)
    {
        _logger = logger;
    }

    [HttpPost("addOne", Name = "AddTodo")]
    public IActionResult AddOne(Todo todo)
    {
        todo.Id = (uint)_data.Count + 1;
        _data.Add(todo);
        return CreatedAtAction("addOne", todo);
    }

    [HttpGet("getOne/{id}", Name = "GetTodo")]
    public IActionResult GetOne(uint id)
    {
        var todo = _data.Find(todo => todo.Id == id);

        if (todo == null)
        {
            _logger.LogInformation("Todo not found {}", todo);
            return NotFound();
        }

        _logger.LogInformation("Todo found {}", todo);
        return Ok(todo);
    }

    [HttpDelete("deleteOne/{id}", Name = "DeleteOne")]
    public IActionResult DeleteOne(uint id)
    {
        var todo = _data.Find(todo => todo.Id == id);

        if (todo == null)
        {
            _logger.LogInformation("Todo not found {}", todo);
            return NotFound();
        }

        _data.Remove(todo);
        _logger.LogInformation("Todo deleted {}", todo);
        return Ok(todo);
    }

    [HttpPut("updateOne/{id}", Name = "UpdateOne")]
    public IActionResult UpdateOne(uint id, Todo newTodo)
    {
        var todo = _data.Find(todo => todo.Id == id);

        if (todo == null)
        {
            _logger.LogInformation("Todo not found {}", todo);
            return NotFound();
        }

        todo.Label = newTodo.Label;
        todo.Description = newTodo.Description;
        
        _logger.LogInformation("Todo updated {}", todo);
        return Ok(todo);
    }

    [HttpGet("getAll", Name = "GetTodos")]
    public IActionResult GetAll()
    {
        _logger.LogInformation("{}", _data);
        return Ok(_data);
    }
}