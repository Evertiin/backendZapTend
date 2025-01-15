using DBZapTend.Logs;
using DBZapTend.Models;
using DBZapTend.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DBZapTend.Controllers
{
    [Authorize(Roles = "User")]
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
            try
            {
                var categories = await _repository.GetCategorys();
                await Log.LogToFile("log_", "");
                return Ok(categories);
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategoryy(Category category)
        {
            try
            {
                if (category is null)
                {
                    return BadRequest("Dados inválidos");
                }

                var createdCategory = await _repository.CreateCategory(category);
                return Ok(createdCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> GetCategoryId(int id)
        {
            try
            {
                var category = await _repository.GetCategory(id);

                if (category == null)
                {
                    return NotFound("Categoria não encontrada");
                }

                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Category>> UpdateCategoryy(int id, Category category)
        {
            try
            {
                if (id != category.IdCategory)
                {
                    return BadRequest("Dados inválidos");
                }

                var updatedCategory = await _repository.UpdateCategory(category);
                return Ok(updatedCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Category>> DeleteCategoryy(int id)
        {
            try
            {
                var category = await _repository.GetCategory(id);

                if (category == null)
                {
                    return NotFound("Categoria não encontrada");
                }

                var deletedCategory = await _repository.DeleteCategory(id);
                return Ok(deletedCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
    }
}