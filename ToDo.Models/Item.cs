using System;
using System.ComponentModel.DataAnnotations;

namespace ToDo.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required, MaxLength(150)]
        public string Owner { get; set; }

        [MaxLength(255)]
        public string Avatar { get; set; }

        [Required]
        public short Status { get; set; }

        [Required]
        public int Hours { get; set; }

        public DateTime DueDate { get; set; }
    }
}