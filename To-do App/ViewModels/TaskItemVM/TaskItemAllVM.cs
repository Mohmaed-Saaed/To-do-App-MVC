
using Microsoft.AspNetCore.Mvc.Rendering;

namespace To_do_App.ViewModels.TaskItemVM
{
    public class TaskItemAllVM
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public DateTime DueDate { get; set; }

        public Priority Priority { get; set; }

        public TaskStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CategoryName { get; set; } = null!;

    }
}
