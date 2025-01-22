using System;
using System.ComponentModel.DataAnnotations;
using backend.Enums;

namespace backend.Data.Dtos
{
    public class ToDoTaskRequestDto
    {
        [Required, MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime? DueDate { get; set; }

        public ItemStatus? ItemStatus { get; set; }
    }
}
