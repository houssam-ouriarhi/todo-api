using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MyApi.Services;

namespace MyApi.Controllers;

[ApiController]
[EnableCors("_myAllowSpecificOrigins")]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private readonly TodoTaskService _todoTaskService;

    private readonly ILogger<TodoController> _logger;

    public TodoController(ILogger<TodoController> logger,
                          TodoTaskService todoTaskService)
    {
        _logger = logger;
        _todoTaskService = todoTaskService;
    }

    [HttpPost("addOne", Name = "AddTodo")]
    public async Task<ActionResult<TodoTask>> AddOne(TodoTask todoTask)
    {
        await _todoTaskService.CreateAsync(todoTask);
        return CreatedAtAction("addOne", todoTask);
    }

    [HttpGet("getOne/{id}", Name = "GetTodo")]
    public async Task<ActionResult<TodoTask>> GetOne(string id)
    {
        var todo = await _todoTaskService.GetAsync(id);

        if (todo == null)
        {
            _logger.LogInformation("Todo not found {}", todo);
            return NotFound();
        }

        _logger.LogInformation("Todo found {}", todo);
        return Ok(todo);
    }

    [HttpDelete("deleteOne/{id}", Name = "DeleteOne")]
    public async Task<ActionResult<TodoTask>> DeleteOne(string id)
    {
        var todo = await _todoTaskService.GetAsync(id);

        if (todo == null)
        {
            _logger.LogInformation("Todo not found {}", todo);
            return NotFound();
        }

        await _todoTaskService.RemoveAsync(id);
        _logger.LogInformation("Todo deleted {}", todo);
        return Ok(todo);
    }

    [HttpPut("updateOne/{id}", Name = "UpdateOne")]
    public async Task<ActionResult<TodoTask>> UpdateOne(string id, TodoTask newTodoTask)
    {
        var todo = await _todoTaskService.GetAsync(id);

        if (todo == null)
        {
            _logger.LogInformation("Todo not found {}", todo);
            return NotFound();
        }

        todo.Label = newTodoTask.Label;
        todo.Description = newTodoTask.Description;
        await _todoTaskService.UpdateAsync(id, todo);
        
        _logger.LogInformation("Todo updated {}", todo);
        return Ok(todo);
    }

    [HttpGet("getAll", Name = "GetTodos")]
    public async Task<ActionResult<List<TodoTask>>> GetAll()
    {
        var tasks = await _todoTaskService.GetAsync();
        
        _logger.LogInformation("{}", tasks);
        return Ok(tasks);
    }
}