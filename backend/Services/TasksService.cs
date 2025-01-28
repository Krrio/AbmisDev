using backend.Data;
using backend.Data.Dtos;
using backend.Data.Models;
using backend.Data.Models.Enums;
using backend.Enums;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class TasksService : ITasksService
    {
        private readonly AppDbContext _context;

        public TasksService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ToDoTaskRequestDto>> GetAllTasksAsync(string? timeRange, DateTime? startDate, DateTime? endDate, int userId, string? sortBy = null, string? sortOrder = "asc")
        {
            var query = _context.Tasks.Where(t => t.UserId == userId).AsQueryable();

            var now = DateTime.UtcNow;

            //Filtrowanie
            switch (timeRange?.ToLower())
            {
                //Zwracamy zadania z dzisiaj
                case "today":
                    query = query.Where(t => t.DueDate.Date == now.Date);
                    break;
                //Zwracamy zadania z jutra
                case "week":
                    var startOfWeek = now.Date.AddDays(-(int)now.DayOfWeek);
                    var endOfWeek = startOfWeek.AddDays(7);
                    query = query.Where(t => t.DueDate.Date >= startOfWeek && t.DueDate.Date < endOfWeek);
                    break;
                // Zwracamy zadania z przeszłości
                case "overdue":
                    query = query.Where(t => t.DueDate.Date < now.Date && t.ItemStatus != ItemStatus.Completed);
                    break;
                // Zwracamy zadania z określonego zakresu dat
                case "custom":
                    if (startDate.HasValue && endDate.HasValue)
                    {
                        query = query.Where(t => t.DueDate.Date >= startDate.Value.Date && t.DueDate.Date <= endDate.Value.Date);
                    }
                    else
                    {
                        throw new ArgumentException("Nie podano zakresu dat.");
                    }
                    break;
                default:
                    break;
            }
            //Sortowanie
            if(!string.IsNullOrEmpty(sortBy))
            {
                switch(sortBy.ToLower())
                {
                    // Sortowanie po dacie
                    case "duedate":
                        query = sortOrder.ToLower() == "desc" 
                        ? query.OrderByDescending(t => t.DueDate)
                        : query.OrderBy(t => t.DueDate);
                    break;
                    // Sortowanie po priorytecie
                    case "priority":
                        query = sortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(t => t.ItemPriority)
                        : query.OrderBy(t => t.ItemPriority);
                    break;
                    // Sortowanie po statusie
                    case "status":
                        query = sortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(t => t.ItemStatus)
                        : query.OrderBy(t => t.ItemStatus);
                    break;

                    default:
                        throw new ArgumentException($"Nieobsługiwany parametr sortowania: {sortBy}");
                }
            }
            // Wykonanie zapytania
            var tasks = await query.ToListAsync();
            // Mapowanie na DTO
            return tasks.Select(t => new ToDoTaskRequestDto
            {
                Title = t.Title,
                Description = t.Description,
                DueDate = t.DueDate,
                Priority = t.ItemPriority,
                ItemStatus = t.ItemStatus
            });
        }
        public async Task<ToDoItem> CreateTaskAsync(ToDoTaskRequestDto request, int userId)
        {
            try
            {
                if (request.DueDate.HasValue && request.DueDate < DateTime.Now)
                    throw new ArgumentException("Data nie może być wcześniejsza niż obecna.");

                if(request.Priority.HasValue && !Enum.IsDefined(typeof(ItemPriority), request.Priority.Value))
                {
                    throw new ArgumentException("Nieprawidłowa wartość priorytetu.");
                }
                var newTask = new ToDoItem
                {
                    ItemPriority = request.Priority ?? ItemPriority.Medium,
                    Title = request.Title,
                    Description = request.Description,
                    DueDate = request.DueDate ?? DateTime.Now,
                    ItemStatus = request.ItemStatus ?? ItemStatus.Pending,
                    UserId = userId
                };

                _context.Tasks.Add(newTask);
                await _context.SaveChangesAsync();

                
                // Sprawdzenie poprawności zapisu
                if (newTask.Id == 0)
                    throw new InvalidOperationException("Nie udało się wygenerować ID dla nowego zadania.");

                return newTask;
            }
            catch (Exception ex)
            {
                // Logowanie szczegółów błędu
                Console.WriteLine($"Błąd podczas zapisu: {ex.Message}");
                if (ex.InnerException != null)
                    Console.WriteLine($"InnerException: {ex.InnerException.Message}");
                throw;
            }
        }

        public async Task<ToDoItem> DeleteTaskAsync(int id, int userId)
        {
            var task = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId) ??
                throw new KeyNotFoundException("Nie znaleziono zadania");

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<ToDoItem> GetTaskByIdAsync(int id, int userId)
        {
            var task = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId) ??
                throw new KeyNotFoundException("Nie znaleziono zadania");

            return task;
        }

        public async Task<ToDoItem> UpdateTaskAsync(int id, ToDoTaskRequestDto request, int userId)
        {
            var task = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId) ??
                throw new KeyNotFoundException("Nie znaleziono zadania");          

            task.Title = request.Title ?? task.Title;
            task.Description = request.Description ?? task.Description;
            task.DueDate = request.DueDate ?? task.DueDate;
            task.ItemStatus = request.ItemStatus ?? task.ItemStatus;

            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();

            return task;
        }
        public async Task<IEnumerable<ToDoItem>> GetTasksByDateAsync(DateTime date, int userId)
        {
            var startDate = date.Date; // Początek dnia
            var endDate = startDate.AddDays(1); // Następny dzień, wyłączony z zakresu

            Console.WriteLine($"Filtrowanie zadań od {startDate} do {endDate} dla użytkownika {userId}");
            return await _context.Tasks
                .Where(t => t.DueDate >= startDate && t.DueDate < endDate && t.UserId == userId)
                .ToListAsync();
        }
    }
}
