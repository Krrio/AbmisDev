using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        [EmailAddress]
        [Required, MaxLength(100)]
        public required string Email { get; set; }
        [Required, MaxLength(100)]
        public required string Password { get; set; }
    }
}