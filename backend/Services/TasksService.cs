using backend.Data;
using backend.Data.Dtos;
using backend.Data.Models;
using backend.Enums;

namespace backend.Services
{
    public class TasksService : ITasksService
    {
        private readonly AppDbContext _context;

        public TasksService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ToDoItem>> GetAllTasksAsync()
        {
            return await Task.FromResult(_context.Tasks.AsEnumerable());
        }

        public async Task<ToDoItem> CreateTaskAsync(ToDoTaskRequestDto request)
        {
            if (request.DueDate.HasValue && request.DueDate < DateTime.Now)
                throw new ArgumentException("Data nie może być wcześniejsza niż obecna.");

            var newTask = new ToDoItem
            {
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate ?? DateTime.Now,
                ItemStatus = request.ItemStatus ?? ItemStatus.Pending
            };

            _context.Tasks.Add(newTask);
            await _context.SaveChangesAsync();

            // Sprawdzenie poprawności zapisu
            if (newTask.Id == 0)
                throw new InvalidOperationException("Nie udało się wygenerować ID dla nowego zadania.");

            return newTask;
        }

        public async Task<ToDoItem> DeleteTaskAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id) ?? throw new KeyNotFoundException();
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<ToDoItem> GetTaskByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id) ?? throw new KeyNotFoundException();
        }

        public async Task<ToDoItem> UpdateTaskAsync(int id, ToDoTaskRequestDto request)
        {
            var task = await _context.Tasks.FindAsync(id) ?? throw new KeyNotFoundException();

            task.Title = request.Title ?? task.Title;
            task.Description = request.Description ?? task.Description;
            task.DueDate = request.DueDate ?? task.DueDate;
            task.ItemStatus = request.ItemStatus ?? task.ItemStatus;

            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();

            return task;
        }
    }
}
