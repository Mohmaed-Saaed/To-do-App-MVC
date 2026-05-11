using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO.TaskItem
{
    public class  DTOGetAllTaskItem
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
