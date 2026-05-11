using Domain.Utilities.Interface;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class ApplicationUser : IdentityUser
    {

        public ICollection<TaskItem> TaskItems { get; set; } = new List<TaskItem>();
    }
}
