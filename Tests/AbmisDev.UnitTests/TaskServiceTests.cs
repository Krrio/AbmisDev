using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Data.Dtos;
using backend.Data.Models;
using backend.Data.Models.Enums;
using backend.Enums;
using backend.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Xunit;
using Xunit.Sdk;

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
        [Fact]
        public async Task CreateTaskWithValidData_ReturnsNewTask()
        { 
            // Arrange
            int userId = 1;
            var options = GetDbOptions();

            using (var context = new AppDbContext(options))
            {
                var service = new TasksService(context);

                var taskRequest = new ToDoTaskRequestDto
                {
                    Title = "Test task",
                    DueDate = DateTime.UtcNow,
                    Priority = ItemPriority.High
                };

                // Act
                var result = await service.CreateTaskAsync(taskRequest, userId);

                // Assert
                result.Should().NotBeNull();
                result.Title.Should().Be("Test task");
                result.UserId.Should().Be(userId);
                result.DueDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(3)); // Upewniamy się, że data jest poprawna
                result.ItemPriority.Should().Be(ItemPriority.High);
            }
        }
        [Fact]
        public async Task CreateTaskWithNoProvidedPriority_ReturnsMedium()
        {
            //Arrange
            int userId = 1;
            var options = GetDbOptions();
            
            using(var context = new AppDbContext(options))
            {
                var service = new TasksService(context);

                var taskRequest = new ToDoTaskRequestDto
                {
                    Title = "Test task",
                    DueDate = DateTime.UtcNow
                };

                //Act 
                var result = await service.CreateTaskAsync(taskRequest, userId);

                //Assert
                result.Should().NotBeNull();
                result.Title.Should().Be("Test task");
                result.UserId.Should().Be(userId);
                result.DueDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
                result.ItemPriority.Should().Be(ItemPriority.Medium);
            }
        }
        [Fact]
        public async Task CreateTaskWithNoProvidedDueDate_ReturnsNow()
        {
            //Arrange
            int userId = 1;
            var options = GetDbOptions();

            using(var context = new AppDbContext(options))
            {
                var service = new TasksService(context);

                var taskRequest = new ToDoTaskRequestDto
                {
                    Title = "Test task"
                };    

                //Act 
                var result = await service.CreateTaskAsync(taskRequest, userId);

                //Assert
                result.Should().NotBeNull();
                result.Title.Should().Be("Test task");
                result.UserId.Should().Be(userId);
                result.DueDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
                result.ItemPriority.Should().Be(ItemPriority.Medium);
            }
        }
        [Fact]
        public async Task DeleteTaskAsync_RemovesTask_WhenTaskExists()
        {
            // Arrange
            int userId = 1;
            var options = GetDbOptions();

            using(var context = new AppDbContext(options))
            {
                var service = new TasksService(context);

                var task = new ToDoItem
                {
                    Title = "Test task",
                    DueDate = DateTime.UtcNow,
                    UserId = userId
                };

                context.Tasks.Add(task);
                await context.SaveChangesAsync();
                // Act
                var result = await service.DeleteTaskAsync(task.Id, userId);

                // Assert
                result.Should().NotBeNull();
                result.Id.Should().Be(task.Id);
                result.Title.Should().Be("Test task");

                var deletedTask = await context.Tasks.FindAsync(task.Id);
                deletedTask.Should().BeNull();
            }
        }
        [Fact]
        public async Task GetTaskByIdAsync_ReturnsTask_WhenTaskExists()
        {
            int userId = 1;
            var options = GetDbOptions();

            using(var context = new AppDbContext(options))
            {
                var service = new TasksService(context);

                var task = new ToDoItem
                {
                    Title = "Test task",
                    DueDate = DateTime.UtcNow,
                    UserId = userId
                };

                context.Tasks.Add(task);
                await context.SaveChangesAsync();

                //Act
                var result = await service.GetTaskByIdAsync(task.Id, userId);

                //Assert
                result.Should().NotBeNull();
                result.Id.Should().Be(task.Id);
                result.Title.Should().Be("Test task");

            }
        }
        [Fact]
        public async Task GetTaskByIdAsync_ReturnsException_WhenTaskDoesNotExist()
        {
            int userId = 1;
            var options = GetDbOptions();

            using(var context = new AppDbContext(options))
            {
                var service = new TasksService(context);

                //Act & Assert

                await FluentActions
                    .Invoking(() => service.GetTaskByIdAsync(1, userId))
                    .Should().ThrowAsync<KeyNotFoundException>();
                
            }
        }
        [Fact]
        public async Task UpdateTaskAsync_UpdatesTask_WhenTaskExists()
        {
            // Arrange
            int userId = 1;
            var options = GetDbOptions();

            using(var context = new AppDbContext(options))
            {
                var service = new TasksService(context);

                var task = new ToDoItem
                {
                    Title = "Test task",
                    DueDate = DateTime.UtcNow,
                    UserId = userId
                };

                context.Tasks.Add(task);
                await context.SaveChangesAsync();

                var taskRequest = new ToDoTaskRequestDto
                {
                    Title = "Updated task",
                    DueDate = DateTime.UtcNow.AddDays(1),
                    Priority = ItemPriority.High
                };
                
                // Act
                var result = await service.UpdateTaskAsync(task.Id, taskRequest, userId);

                // Assert
                result.Should().NotBeNull();
                result.Id.Should().Be(task.Id);
                result.Title.Should().Be("Updated task");
                result.DueDate.Should().BeCloseTo(DateTime.UtcNow.AddDays(1), TimeSpan.FromSeconds(1));
                result.ItemPriority.Should().Be(ItemPriority.High);
            }
        }
        [Fact]
        public async Task UpdateTaskAsync_ReturnsException_WhenTaskDoesNotExist()
        {
            // Arrange

            int userId = 1;
            var options = GetDbOptions();

            using(var context = new AppDbContext(options))
            {
                var service = new TasksService(context);

                var taskRequest = new ToDoTaskRequestDto
                {
                    Title = "Updated task",
                    DueDate = DateTime.UtcNow.AddDays(1),
                    Priority = ItemPriority.High
                };

                //Act & Assert
                await FluentActions
                    .Invoking(() => service.UpdateTaskAsync(1, taskRequest, userId))
                    .Should().ThrowAsync<KeyNotFoundException>();
            }
        }
        [Fact]
        public async Task GetTasksByDateAsync_ReturnsOnlyProvidedDate()
        {
            int userId = 1;
            var options = GetDbOptions();

            using(var context = new AppDbContext(options))
            {
                var service = new TasksService(context);
                var today = DateTime.UtcNow;

                context.Tasks.Add(new ToDoItem
                {
                    Title = "Test task",
                    DueDate = DateTime.UtcNow,
                    UserId = userId
                });

                context.Tasks.Add(new ToDoItem
                {
                    Title = "Test task",
                    DueDate = today.AddDays(1),
                    UserId = userId
                });

                context.Tasks.Add(new ToDoItem
                {
                    Title = "Test task",
                    DueDate = today.AddDays(-1),
                    UserId = userId
                });

                await context.SaveChangesAsync();

                // Act

                var result = await service.GetTasksByDateAsync(today, userId);

                // Assert
                result.Should().HaveCount(1);
                result.First().Title.Should().Be("Test task");
                result.First().DueDate.Should().BeCloseTo(today, TimeSpan.FromSeconds(1));

            }
        }
        [Fact]
        public async Task GetTasksByDateAsync_returnsEmptyList_WhenNoTasksExist()
        {
            int userId = 1;
            var options = GetDbOptions();

            using(var context = new AppDbContext(options))
            {
                var service = new TasksService(context);
                var today = DateTime.UtcNow.Date;

                context.Tasks.Add(new ToDoItem
                {
                    Title = "Test task",
                    DueDate = today.AddDays(140),
                    UserId = userId
                });

                context.Tasks.Add(new ToDoItem
                {
                    Title = "Test task",
                    DueDate = today.AddDays(1),
                    UserId = userId
                });

                context.Tasks.Add(new ToDoItem
                {
                    Title = "Test task",
                    DueDate = today.AddDays(-1),
                    UserId = userId
                });

                await context.SaveChangesAsync();

                // Act
                var result = await service.GetTasksByDateAsync(today, userId);

                // Assert
                result.Should().BeEmpty();

            }
        }
    }
}