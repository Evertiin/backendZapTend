using DBZapTend.Logs;
using DBZapTend.Models;
using DBZapTend.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DBZapTend.Controllers
{
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
                await Log.LogToFile("log_", "Todas as instancias coletadas");

                return Ok(instances);
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1002-5 ,Erro interno do servidor: {ex.Message}");
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Instance>> CreateInstance(Instance instance)
        {
            try
            {
                if (string.IsNullOrEmpty(instance.Name))
                {
                    await Log.LogToFile("log_", "Dados inválidos");
                    return BadRequest("Dados inválidos");
                }


                var createdInstance = await _repository.CreateInstance(instance);
                await Log.LogToFile("log_", "Instancia criada");
                return Ok(createdInstance);

            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1002-5 ,Erro interno do servidor: {ex.Message}");
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
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
                    await Log.LogToFile("log_", "Instância não encontrada");
                    return NotFound("Instância não encontrada");
                }

                return Ok(instance);
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1002-5 ,Erro interno do servidor: {ex.Message}");
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Instance>> UpdateInstance(int id, Instance instance)
        {
            try
            {
                if (string.IsNullOrEmpty(instance.Name))
                {
                    await Log.LogToFile("log_", "Dados inválidos");
                    return BadRequest("Dados inválidos");
                }

                if (id != instance.Id)
                {
                    await Log.LogToFile("log_", "Dados inválidos");
                    return BadRequest("Dados inválidos");
                }

                var updatedInstance = await _repository.UpdateInstance(instance);
                await Log.LogToFile("log_", "Instancia atuaizada com sucesso");
                return Ok(updatedInstance);
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1002-5 ,Erro interno do servidor: {ex.Message}");
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
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
                    await Log.LogToFile("log_", "Instância não encontrada");
                    return NotFound("Instância não encontrada");
                }

                var deleteResult = await _repository.DeleteInstance(id);
                await Log.LogToFile("log_", "Instancia deletada com sucesso");
                return Ok(deleteResult);
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1002-5 ,Erro interno do servidor: {ex.Message}");
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
    }
}