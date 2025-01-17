using DBZapTend.Logs;
using DBZapTend.Models;
using DBZapTend.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DBZapTend.Controllers
{
    [Authorize(Roles = "User,Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class InstanceController : Controller
    {
        private readonly IInstanceRepository _repository;

        public InstanceController(IInstanceRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Instance>>> GetInstances()
        {
            try
            {
                var instances = await _repository.GetInstances();
                await Log.LogToFile("log_", "COD:1003-2 ,Todas as instâncias coletadas com sucesso");
                return Ok(new { Message = "COD:1003-2 ,Todas as instâncias coletadas com sucesso", Instances = instances });
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1003-5 ,Erro interno ao buscar instâncias: {ex.Message}");
                return StatusCode(500, $"COD:1003-5 ,Erro interno do servidor");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Instance>> CreateInstance(Instance instance)
        {
            try
            {
                if (string.IsNullOrEmpty(instance.Name))
                {
                    await Log.LogToFile("log_", "COD:1003-4 ,Dados inválidos ao criar instância");
                    return BadRequest("COD:1003-4 ,Dados inválidos");
                }

                var createdInstance = await _repository.CreateInstance(instance);
                await Log.LogToFile("log_", $"COD:1003-2 ,Instância criada com sucesso");
                return Ok(new { Message = "COD:1003-2 ,Instância criada com sucesso", Instance = createdInstance });
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1003-5 ,Erro interno ao criar instância: {ex.Message}");
                return StatusCode(500, $"COD:1003-5 ,Erro interno do servidor");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Instance>> GetInstanceId(int id)
        {
            try
            {
                var instance = await _repository.GetInstance(id);

                if (instance == null)
                {
                    await Log.LogToFile("log_", $"COD:1003-4 ,Instância não encontrada: {id}");
                    return NotFound("COD:1003-4 ,Instância não encontrada");
                }

                await Log.LogToFile("log_", $"COD:1003-2 ,Instância coletada com sucesso: {id}");
                return Ok(new { Message = "COD:1003-2 ,Instância coletada com sucesso", Instance = instance });
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1003-5 ,Erro interno ao buscar instância: {ex.Message}");
                return StatusCode(500, $"COD:1003-5 ,Erro interno do servidor");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Instance>> UpdateInstance(int id, Instance instance)
        {
            try
            {
                if (string.IsNullOrEmpty(instance.Name))
                {
                    await Log.LogToFile("log_", "COD:1003-4 ,Dados inválidos ao atualizar instância");
                    return BadRequest("COD:1003-4 ,Dados inválidos");
                }

                if (id != instance.Id)
                {
                    await Log.LogToFile("log_", "COD:1003-4 ,Dados inválidos ao atualizar instância");
                    return BadRequest("COD:1003-4 ,Dados inválidos");
                }

                var updatedInstance = await _repository.UpdateInstance(instance);
                await Log.LogToFile("log_", $"COD:1003-2 ,Instância atualizada com sucesso: {id}");
                return Ok(new { Message = "COD:1003-2 ,Instância atualizada com sucesso", Instance = updatedInstance });
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1003-5 ,Erro interno ao atualizar instância: {ex.Message}");
                return StatusCode(500, $"COD:1003-5 ,Erro interno do servidor");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Instance>> DeleteInstance(int id)
        {
            try
            {
                var instance = await _repository.GetInstance(id);

                if (instance == null)
                {
                    await Log.LogToFile("log_", $"COD:1003-4 ,Instância não encontrada: {id}");
                    return NotFound("COD:1003-4 ,Instância não encontrada");
                }

                var deleteResult = await _repository.DeleteInstance(id);
                await Log.LogToFile("log_", $"COD:1003-2 ,Instância deletada com sucesso: {id}");
                return Ok(new { Message = "COD:1003-2 ,Instância deletada com sucesso", Instance = deleteResult });
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1003-5 ,Erro interno ao deletar instância: {ex.Message}");
                return StatusCode(500, $"COD:1003-5 ,Erro interno do servidor");
            }
        }
    }
}