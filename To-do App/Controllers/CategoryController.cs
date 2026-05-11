
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace To_do_App.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var allCategories = (await _categoryService.GetAll()).Adapt<IEnumerable<CategoryViewModel>>();
            return View(allCategories);
        }

        [HttpGet]
        public async Task<ActionResult> Save(int id)
        {

            var category = new CreateViewModel();

            if (id > 0)
            {
                category = (await _categoryService.GetOne(id)).Adapt<CreateViewModel>();
                if (category != null)
                {
                    return View(category);
                }
            }

            return View(category);
        }


        [HttpPost]
        public async Task<ActionResult> Save(CreateViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var category = categoryViewModel.Adapt<DTOCreateCategory>();
                if (category.Id > 0)
                {
                    await _categoryService.Update(category);
                }
                else
                {
                    await _categoryService.Create(category);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoryViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            if (id > 0)
            {
                await _categoryService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            return BadRequest();
        }
    }
}