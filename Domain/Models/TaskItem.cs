using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using TaskStatus = Domain.Enums.TaskStatus;

namespace Domain.Models
{
        public class TaskItem
        {
            public int Id { get; set; }

            [Required]
            [MaxLength(150)]
            public string Title { get; set; }

            [MaxLength(1000)]
            public string? Description { get; set; }

            public DateTime DueDate { get; set; }

            public Priority Priority { get; set; }

            public TaskStatus Status { get; set; }

            public DateTime CreatedAt { get; set; } = DateTime.Now;
            public int CategoryId { get; set; }

            //[ForeignKey(nameof(ApplicationUser))]
            public string UserId { get; set; } = null!;
            public ApplicationUser User { get; set; } = null!;
            public  Category? Category { get; set; }

        }
    
}
