using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data.Dtos;
using backend.Data.Models;

namespace backend.Services
{
    public interface ITasksService
    {
        Task<IEnumerable<ToDoTaskRequestDto>> GetAllTasksAsync(string? timeRange, DateTime? startDate, DateTime? endDate, int userId, string? sortBy = null, string? sortOrder = "asc");
        Task<ToDoItem> GetTaskByIdAsync(int id, int userId);
        Task<ToDoItem> CreateTaskAsync(ToDoTaskRequestDto request, int userId);
        Task<ToDoItem> DeleteTaskAsync(int id, int userId);
        Task<ToDoItem> UpdateTaskAsync(int id, ToDoTaskRequestDto request, int userId);
        Task<IEnumerable<ToDoItem>> GetTasksByDateAsync(DateTime date, int userId);
    }
}