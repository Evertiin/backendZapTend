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
    [Authorize(Roles = "Admin")]
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
                await Log.LogToFile("log_", "COD:1002-2 ,Categorias coletadas com sucesso");
                return Ok(new { Message = "COD:1001-2 ,Categorias coletada com sucesso", Category = categories });
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1002-5 ,Erro interno ao buscar categorias: {ex.Message}");
                return StatusCode(500, $"COD:1002-5 ,Erro interno do servidor");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategoryy(Category category)
        {
            try
            {
                if (category is null)
                {
                    await Log.LogToFile("log_", "COD:1002-4 ,Dados inválidos ao criar categoria");
                    return BadRequest("COD:1002-4 ,Dados inválidos");
                }

                var createdCategory = await _repository.CreateCategory(category);
                await Log.LogToFile("log_", $"COD:1002-2 ,Categoria criada com sucesso");
                return Ok(new { Message = "COD:1001-2 ,Categoria criada com sucesso", Category = createdCategory });
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1002-5 ,Erro interno ao criar categoria: {ex.Message}");
                return StatusCode(500, $"COD:1002-5 ,Erro interno do servidor");
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
                    await Log.LogToFile("log_", $"COD:1002-4 ,Categoria não encontrada: {id}");
                    return NotFound("COD:1002-4 ,Categoria não encontrada");
                }

                await Log.LogToFile("log_", $"COD:1002-2 ,Categoria coletada com sucesso: {id}");
                return Ok(category);
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1002-5 ,Erro interno ao buscar categoria: {ex.Message}");
                return StatusCode(500, $"COD:1002-5 ,Erro interno do servidor");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Category>> UpdateCategoryy(int id, Category category)
        {
            try
            {
                if (id != category.IdCategory)
                {
                    await Log.LogToFile("log_", "COD:1002-4 ,Dados inválidos ao atualizar categoria");
                    return BadRequest("COD:1002-4 ,Dados inválidos");
                }

                var updatedCategory = await _repository.UpdateCategory(category);
                await Log.LogToFile("log_", $"COD:1002-2 ,Categoria atualizada com sucesso: {id}");
                return Ok(updatedCategory);
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1002-5 ,Erro interno ao atualizar categoria: {ex.Message}");
                return StatusCode(500, $"COD:1002-5 ,Erro interno do servidor");
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
                    await Log.LogToFile("log_", $"COD:1002-4 ,Categoria não encontrada: {id}");
                    return NotFound("COD:1002-4 ,Categoria não encontrada");
                }

                var deletedCategory = await _repository.DeleteCategory(id);
                await Log.LogToFile("log_", $"COD:1002-2 ,Categoria deletada com sucesso: {id}");
                return Ok(deletedCategory);
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1002-5 ,Erro interno ao deletar categoria: {ex.Message}");
                return StatusCode(500, $"COD:1002-5 ,Erro interno do servidor");
            }
        }
    }
}