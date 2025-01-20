using System;
using System.ComponentModel.DataAnnotations;
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
        public DateTime DueDate { get; set; }
        public ItemStatus ItemStatus { get; set; } = ItemStatus.Pending; 

        public ToDoItem() { }
    }
}
