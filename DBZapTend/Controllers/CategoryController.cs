using DBZapTend.Models;
using DBZapTend.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DBZapTend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _repository;

        public CategoryController(ICategoryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            var category = await _repository.GetCategorys();
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategoryy(Category category)
        {
            var createdCategory =  await _repository.CreateCategory(category);

            
            return Ok(createdCategory);
        }

    }
}
