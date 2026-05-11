using BLL.DTO.Category;
using BLL.DTO.TaskItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITaskItemService
    {


        Task<IEnumerable<DTOGetAllTaskItem>> GetAll(string userId);
        Task<DTOSaveTaskItem> GetOne(int id);
        Task<bool> Update(DTOSaveTaskItem taskItem);
        Task<bool> Create(DTOSaveTaskItem taskItem);
        Task<bool> Delete(int id);

    }
}
