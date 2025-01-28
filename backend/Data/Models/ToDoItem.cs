using System;
using System.ComponentModel.DataAnnotations;
using backend.Data.Models.Enums;
using backend.Enums;

namespace backend.Data.Models
{
    public class ToDoItem
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime DueDate { get; set; } = DateTime.UtcNow;
        public ItemStatus ItemStatus { get; set; } = ItemStatus.Pending; 
        public int UserId { get; set; }
        public User User { get; set; } 
        public ItemPriority ItemPriority { get; set; } 
        public ToDoItem() { }
    }
}
