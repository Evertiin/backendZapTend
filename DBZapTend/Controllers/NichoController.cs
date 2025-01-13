using DBZapTend.Models;
using DBZapTend.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DBZapTend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NichoController : Controller
    {
        private readonly INichosRepository _repository;

        public NichoController(INichosRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Nicho>>> GetNichos()
        {
            try
            {
                var nicho = await _repository.GetNichos();
                return Ok(nicho);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao buscar nichos: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Nicho>> CreateNicho(Nicho nicho)
        {
            try
            {
                if (nicho is null)
                {
                    return BadRequest("Dados inválidos");
                }

                var createdNicho = await _repository.CreateNicho(nicho);
                return Ok(createdNicho);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao criar nicho: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Nicho>> GetNichoId(int id)
        {
            try
            {
                var nicho = await _repository.GetNicho(id);

                if (nicho == null)
                {
                    return NotFound("Nicho não encontrado");
                }

                return Ok(nicho);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao buscar nicho por ID: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Nicho>> UpdateNicho(int id, Nicho nicho)
        {
            try
            {
                if (id != nicho.IdNichos)
                {
                    return BadRequest("Dados inválidos");
                }

                var updateNicho = await _repository.UpdateNicho(nicho);
                return Ok(updateNicho);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao atualizar nicho: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Nicho>> DeleteNicho(int id)
        {
            try
            {
                var nicho = await _repository.GetNicho(id);

                if (nicho == null)
                {
                    return NotFound("Nicho não encontrado");
                }

                var deleteNicho = await _repository.DeleteNicho(id);
                return Ok(deleteNicho);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao deletar nicho: {ex.Message}");
            }
        }
    }
}