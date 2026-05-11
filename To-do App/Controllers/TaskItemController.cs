

namespace To_do_App.Controllers
{
    [Authorize(Roles = "User")]
    public class TaskItemController : Controller
    {

        private readonly ITaskItemService _taskItemService;
        private readonly ICategoryService _categoryService;
       
        public TaskItemController(ITaskItemService taskItemService, ICategoryService categoryService)
        {
            _taskItemService = taskItemService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
         {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                
            if(userId == null)
                return Unauthorized();

            var allTaskItems = (await _taskItemService.GetAll(userId)).Adapt<List<DTOGetAllTaskItem>>();

            return View(allTaskItems);
        }

        [HttpGet]
        public async Task<IActionResult> Save(int id)
        {
            SaveTaskItemVM taskItem ;

            if (id > 0)
            {
                taskItem = (await _taskItemService.GetOne(id)).Adapt<SaveTaskItemVM>();
            } else
                taskItem = new SaveTaskItemVM();
            
            taskItem.CategoryList = (await _categoryService.GetAll()).Select(
                c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList();

            return View(taskItem);
        }

        [HttpPost]
        public async Task<IActionResult> Save(SaveTaskItemVM model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if(userId == null)
                    return Unauthorized();
                
                var taskItem = model.Adapt<DTOSaveTaskItem>();
                taskItem.UserId = userId;
                if (model.Id > 0)
                {
                    await _taskItemService.Update(taskItem);
                }
                else
                {
                    await _taskItemService.Create(taskItem);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return NotFound("Invalid task ID");
        
              var isDeleted =   await _taskItemService.Delete(id);

            if (isDeleted)
                return RedirectToAction(nameof(Index));

            return NotFound("Failed to delete the task");
        }
    }
}
