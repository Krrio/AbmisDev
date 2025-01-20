using backend.Data.Dtos;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class TasksController : Controller
    {
        private readonly ITasksService _tasksService;

        public TasksController(ITasksService tasksService)
        {
            _tasksService = tasksService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllTasksAsync()
        {
            try
            {
                var tasks = await _tasksService.GetAllTasksAsync();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Wystąpił błąd podczas pobierania zadań.", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskByIdAsync(int id)
        {
            try
            {
                var task = await _tasksService.GetTaskByIdAsync(id);
                return Ok(task);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Nie znaleziono zadania o podanym ID." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Wystąpił błąd podczas pobierania zadania.", error = ex.Message });
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> CreateTaskAsync([FromBody] ToDoTaskRequestDto request)
        {
            try
            {
                var createdTask = await _tasksService.CreateTaskAsync(request);
                return Ok(new { message = "Zadanie utworzone pomyślnie.", task = createdTask });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Wystąpił błąd podczas tworzenia zadania.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskAsync(int id)
        {
            try
            {
                var deletedTask = await _tasksService.DeleteTaskAsync(id);
                return Ok(new { message = "Zadanie usunięte pomyślnie.", task = deletedTask });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Nie znaleziono zadania o podanym ID." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Wystąpił błąd podczas usuwania zadania.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaskAsync(int id, [FromBody] ToDoTaskRequestDto request)
        {
            try
            {
                var updatedTask = await _tasksService.UpdateTaskAsync(id, request);
                return Ok(new { message = "Zadanie zaktualizowane pomyślnie.", task = updatedTask });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Nie znaleziono zadania o podanym ID." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Wystąpił błąd podczas aktualizacji zadania.", error = ex.Message });
            }
        }
    }
}
