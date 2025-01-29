using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Data.Models;
using backend.Enums;
using backend.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AbmisDev.UnitTests
{
    public class TaskServiceTests
    {
        private DbContextOptions<AppDbContext> GetDbOptions()
        {
            return new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unikalna nazwa dla każdego testu
                .Options;
        }

        [Fact]
        public async Task GetAllTasksAsync_TimeRangeToday_ReturnsTasksDueToday()
        {
            // Arrange
            var userId = 1;
            var today = DateTime.UtcNow.Date; // Użyj tej samej wartości, co w serwisie
            var options = GetDbOptions();

            using (var context = new AppDbContext(options))
            {
                context.Tasks.AddRange(new List<ToDoItem>
                {
                    new ToDoItem { UserId = userId, DueDate = today.AddHours(12) }, // Dzisiaj
                    new ToDoItem { UserId = userId, DueDate = today.AddDays(-1) },  // Wczoraj
                    new ToDoItem { UserId = userId, DueDate = today.AddDays(1) }    // Jutro
                });
                context.SaveChanges();

                var service = new TasksService(context);

                // Act
                var result = await service.GetAllTasksAsync("today", null, null, userId);

                // Assert
                result.Should().HaveCount(1);
                result.First().DueDate.Value.Date.Should().Be(today);
            }
        }


        [Fact]
        public async Task GetAllTasksAsync_TimeRangeWeek_ReturnsTasksDueThisWeek()
        {
            // Arrange 
            var userId = 1;
            var today = DateTime.UtcNow.Date;
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
            var endOfWeek = startOfWeek.AddDays(7);
            var options = GetDbOptions();

            using (var context = new AppDbContext(options))
            {
                context.Tasks.AddRange(new List<ToDoItem>
                {
                    new ToDoItem {UserId = userId, DueDate = startOfWeek}, 
                    new ToDoItem {UserId = userId, DueDate = startOfWeek.AddDays(2)}, 
                    new ToDoItem {UserId = userId, DueDate = startOfWeek.AddDays(5)}, 
                    new ToDoItem {UserId = userId, DueDate = endOfWeek.AddDays(-1)}, 
                    new ToDoItem {UserId = userId, DueDate = endOfWeek},
                });
                context.SaveChanges();

                var service = new TasksService(context);

                // Act
                var result = await service.GetAllTasksAsync("week", null, null, userId);

                // Assert
                result.Should().HaveCount(4); // 10.10, 12.10, 09.10, 15.10
            }
        }

        [Fact]
        public async Task GetAllTasksAsync_TimeRangeOverdue_ReturnsOverdueTasks()
        {
            // Arrange
            var userId = 1;
            var today = DateTime.UtcNow.Date;
            var options = GetDbOptions();

            using (var context = new AppDbContext(options))
            {
                context.Tasks.AddRange(new List<ToDoItem>
                {
                    new ToDoItem {UserId = userId, DueDate = today.AddDays(-2), ItemStatus = ItemStatus.Pending}, // Przeterminowane
                    new ToDoItem {UserId = userId, DueDate = today.AddDays(-1), ItemStatus = ItemStatus.Completed}, // Przeterminowane, ale skończone
                    new ToDoItem {UserId = userId, DueDate = today.AddDays(1), ItemStatus = ItemStatus.Pending}, // Nieprzeterminowane
                });
                context.SaveChanges();

                var service = new TasksService(context);

                // Act
                var result = await service.GetAllTasksAsync("overdue", null, null, userId);

                // Assert
                result.Should().HaveCount(1); // Tylko pierwsze zadanie
            }
        }

        [Fact]
        public async Task GetAllTasksAsync_SortByDueDateDesc_ReturnsTasksInCorrectOrder()
        {
            // Arrange
            var userId = 1;
            var fixedDate = new DateTime(2023, 10, 10);
            var options = GetDbOptions();

            using (var context = new AppDbContext(options))
            {
                context.Tasks.AddRange(new List<ToDoItem>
                {
                    new ToDoItem { UserId = userId, DueDate = fixedDate.AddDays(3)}, // 13.10
                    new ToDoItem { UserId = userId, DueDate = fixedDate.AddDays(1)}, // 11.10
                    new ToDoItem { UserId = userId, DueDate = fixedDate.AddDays(5)}, // 15.10
                });
                context.SaveChanges();

                var service = new TasksService(context);

                // Act 
                var result = await service.GetAllTasksAsync(null, null, null, userId, "duedate", "desc");

                // Assert
                result.Should().BeInDescendingOrder(t => t.DueDate);
                result.First().DueDate.Should().Be(fixedDate.AddDays(5)); // 15.10
            }
        }
    }
}
