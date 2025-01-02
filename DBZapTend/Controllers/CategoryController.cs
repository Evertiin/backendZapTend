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
            if (category is null)
            {
                return BadRequest("Dados inválidos");
            }

            var createdCategory =  await _repository.CreateCategory(category);

            
            return Ok(createdCategory);
        }

        [HttpGet("{id:int}")]

        public async Task<ActionResult<Category>> GetCategoryId(int id)
        {
            var category = await _repository.GetCategory(id);

            if (category == null)
            {
                return NotFound("Categoria não encontrada");
            }


            return Ok(category);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Category>> UpdateCategoryy(int id,Category category)
        {
            if (id != category.IdCategory)
            {
                return BadRequest("Dados inválidos");
            }

            var updateCategory = await _repository.UpdateCategory(category);


            return Ok(updateCategory);
        }

       [HttpDelete("{id:int}")]
        public async Task<ActionResult<Category>> DeleteCategoryy(int id)
        {
            var category = _repository.GetCategory(id);

            if (category == null)
            {
                return NotFound("Categoria não encontrada");
            }
            var deleteCategory = await _repository.DeleteCategory(id);


            return Ok(deleteCategory);
        }


    }
}
