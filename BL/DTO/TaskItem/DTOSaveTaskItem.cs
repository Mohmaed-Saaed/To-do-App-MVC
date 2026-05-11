
using System.ComponentModel.DataAnnotations;

namespace BLL.DTO.TaskItem
{
    public class DTOSaveTaskItem
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = null!;

        [MaxLength(1000)]
        public string? Description { get; set; }

        public DateTime DueDate { get; set; }

        public Priority Priority { get; set; }

        public TaskStatus Status { get; set; }

        public DateTime CreatedAt { get; set; } 

        public string UserId { get; set; } = null!;
        public int CategoryId { get; set; }

    }
}
