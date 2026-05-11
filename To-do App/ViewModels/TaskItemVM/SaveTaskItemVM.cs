using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace To_do_App.ViewModels.TaskItemVM
{
    public class SaveTaskItemVM
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = null!;

        [MaxLength(1000)]
        public string? Description { get; set; }

        public DateTime DueDate { get; set; } = DateTime.Now.Date;

        public Priority Priority { get; set; }

        public System.Threading.Tasks.TaskStatus Status { get; set; }

        public int CategoryId { get; set; }

        //public virtual Category UserId { get; set; }
        public List<SelectListItem> CategoryList { get; set; } = new();


    }
}
