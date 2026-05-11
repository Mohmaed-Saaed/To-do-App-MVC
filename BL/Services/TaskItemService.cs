
using System.Reflection.Metadata.Ecma335;

namespace BLL.Services
{
    public class TaskItemService : ITaskItemService
    {

        private readonly IUnitOfWork  _unitOfWork;
        public TaskItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Create(DTOSaveTaskItem taskItem)
        {
            if (await _unitOfWork.TaskItems.CreateAsync(taskItem.Adapt<TaskItem>()))
                if (await _unitOfWork.SaveAsync() > 0)
                    return true;

            return false;
                
            
        }

        public async Task<bool> Delete(int id)
        {
            if (id <= 0)
                throw new Exception("Invalid task ID");

            var taskItem = await _unitOfWork.TaskItems.GetOneAsync(t => t.Id == id);

            if (taskItem == null)
                throw new Exception("Task not found");

            if (await _unitOfWork.TaskItems.DeleteAsync(taskItem))
                return await _unitOfWork.SaveAsync() > 0;

            return false;
        }

        public async Task<DTOSaveTaskItem> GetOne(int id)
        {
            var taskItem = await _unitOfWork.TaskItems.GetOneAsync(t => t.Id == id);

            if (taskItem == null)
                throw new Exception("Task not found");

             return  taskItem.Adapt<DTOSaveTaskItem>();
        }

        public async Task<bool> Update(DTOSaveTaskItem taskItem)
        {

            if(taskItem.Id <= 0)
                throw new Exception("Invalid task ID");

            if (await _unitOfWork.TaskItems.UpdateAsync(taskItem.Adapt<TaskItem>()))
                return await _unitOfWork.SaveAsync() > 0;

            return false;
         }

        async Task<IEnumerable<DTOGetAllTaskItem>> ITaskItemService.GetAll(string userId)
        {
            

            var taskItems = await _unitOfWork.TaskItems.GetAsync(t => t.UserId == userId, include: [t => t.Category]);
            return taskItems.Adapt<IEnumerable<DTOGetAllTaskItem>>();
        }

    }
}
