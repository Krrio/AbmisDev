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
        Task<IEnumerable<ToDoItem>> GetAllTasksAsync();
        Task<ToDoItem> GetTaskByIdAsync(int id);
        Task<ToDoItem> CreateTaskAsync(ToDoTaskRequestDto request);
        Task<ToDoItem> DeleteTaskAsync(int id);
        Task<ToDoItem> UpdateTaskAsync(int id, ToDoTaskRequestDto request);

    }
}