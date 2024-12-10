using DBZapTend.Models;
using DBZapTend.Repository;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult <IEnumerable<Category>> Get()
        {
            var category = _repository.GetCategorys();
            return Ok(category);
        }
    }
}
