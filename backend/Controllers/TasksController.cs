using System.Security.Claims;
using backend.Data.Dtos;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Authorize] //Endpointy w tym kontrolerze wymagają autoryzacji (użytkownik musi się zalogować, żeby otrzymać dostęp)
    [ApiController]
    [Route("api/[controller]")]
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
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0"); // Pobieramy userId z tokena JWT
                Console.WriteLine($"UserId Claim: {userId}");
                System.Console.WriteLine($"Nameidentifier: {userId}");
                var tasks = await _tasksService.GetAllTasksAsync(userId);
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
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var task = await _tasksService.GetTaskByIdAsync(id, userId);
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
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0"); // Pobieramy userId z tokena JWT
                var createdTask = await _tasksService.CreateTaskAsync(request, userId);
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
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var deletedTask = await _tasksService.DeleteTaskAsync(id, userId);
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
            {   var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var updatedTask = await _tasksService.UpdateTaskAsync(id, request, userId);
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
        [HttpGet("date/{date}")]
        public async Task<IActionResult> GetTasksByDateAsync(DateTime date)
        {
            try 
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var tasks = await _tasksService.GetTasksByDateAsync(date, userId);

                if(!tasks.Any())
                {
                    return NotFound(new { message = "Nie znaleziono zadań na podaną datę." });
                }
                return Ok(tasks);
            }
            catch(Exception ex){
                return StatusCode(500, new { message = "Wystąpił błąd podczas pobierania zadań.", error = ex.Message });
            }
        }
    }
}
