using backend.Data;
using backend.Data.Dtos;
using backend.Data.Models;
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

        public async Task<IEnumerable<ToDoItem>> GetAllTasksAsync(int userId)
        {
            return await _context.Tasks
            .Where(t => t.UserId == userId) // Filtrujemy taski po userId
            .ToListAsync();
        }
        // Metoda zwracająca listę zadań z podziałem na strony - Jeszcze nie wiem jak to zastosować w kalendarzu żeby to miało sens
        /*  public async Task<IEnumerable<ToDoItem>> GetAllTaskByAsync(int pageNumber, int pageSize)
        {
            return await _context.Tasks
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        } */
        public async Task<ToDoItem> CreateTaskAsync(ToDoTaskRequestDto request, int userId)
        {
            if (request.DueDate.HasValue && request.DueDate < DateTime.Now)
                throw new ArgumentException("Data nie może być wcześniejsza niż obecna.");

            var newTask = new ToDoItem
            {
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
