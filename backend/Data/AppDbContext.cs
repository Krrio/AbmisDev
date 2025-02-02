using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Controllers;
using backend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<ToDoItem> Tasks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoItem>()
                .HasOne(t => t.User)
                .WithMany(u => u.ToDoItems)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().HasData(
                new User{
                    Id = 1,
                    Email = "test@gmail.com",
                    Password = "test123"
                }
            );
        }
    }
}