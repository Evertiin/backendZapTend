using DBZapTend.Logs;
using DBZapTend.Models;
using DBZapTend.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DBZapTend.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {
        private readonly IPaymentRepository _repository;

        public PaymentController(IPaymentRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
        {
            try
            {
                var payments = await _repository.GetPayments();
                await Log.LogToFile("log_", "COD:1005-2 ,Pagamentos coletados com sucesso");
                return Ok(new { Message = "COD:1005-2 ,Pagamentos coletados com sucesso", Payments = payments });
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1005-5 ,Erro interno ao buscar pagamentos: {ex.Message}");
                return StatusCode(500, $"COD:1005-5 ,Erro interno do servidor");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Payment>> CreatePayment(Payment payment)
        {
            try
            {
                if (payment is null)
                {
                    await Log.LogToFile("log_", "COD:1005-4 ,Dados inválidos ao criar pagamento");
                    return BadRequest("COD:1005-4 ,Dados inválidos");
                }

                var createdPayment = await _repository.CreatePayment(payment);
                await Log.LogToFile("log_", $"COD:1005-2 ,Pagamento criado com sucesso");
                return Ok(new { Message = "COD:1005-2 ,Pagamento criado com sucesso", Payment = createdPayment });
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1005-5 ,Erro interno ao criar pagamento: {ex.Message}");
                return StatusCode(500, $"COD:1005-5 ,Erro interno do servidor");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Payment>> GetPaymentId(int id)
        {
            try
            {
                var payment = await _repository.GetPayment(id);

                if (payment == null)
                {
                    await Log.LogToFile("log_", $"COD:1005-4 ,Pagamento não encontrado: {id}");
                    return NotFound("COD:1005-4 ,Pagamento não encontrado");
                }

                await Log.LogToFile("log_", $"COD:1005-2 ,Pagamento coletado com sucesso: {id}");
                return Ok(new { Message = "COD:1005-2 ,Pagamento coletado com sucesso", Payment = payment });
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1005-5 ,Erro interno ao buscar pagamento por ID: {ex.Message}");
                return StatusCode(500, $"COD:1005-5 ,Erro interno do servidor");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Payment>> UpdatePayment(int id, Payment payment)
        {
            try
            {
                if (id != payment.IdPayments)
                {
                    await Log.LogToFile("log_", "COD:1005-4 ,Dados inválidos ao atualizar pagamento");
                    return BadRequest("COD:1005-4 ,Dados inválidos");
                }

                var updatedPayment = await _repository.UpdatePayment(payment);
                await Log.LogToFile("log_", $"COD:1005-2 ,Pagamento atualizado com sucesso: {id}");
                return Ok(new { Message = "COD:1005-2 ,Pagamento atualizado com sucesso", Payment = updatedPayment });
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1005-5 ,Erro interno ao atualizar pagamento: {ex.Message}");
                return StatusCode(500, $"COD:1005-5 ,Erro interno do servidor");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Payment>> DeletePayment(int id)
        {
            try
            {
                var payment = await _repository.GetPayment(id);

                if (payment == null)
                {
                    await Log.LogToFile("log_", $"COD:1005-4 ,Pagamento não encontrado: {id}");
                    return NotFound("COD:1005-4 ,Pagamento não encontrado");
                }

                var deletePayment = await _repository.DeletePayment(id);
                await Log.LogToFile("log_", $"COD:1005-2 ,Pagamento deletado com sucesso: {id}");
                return Ok(new { Message = "COD:1005-2 ,Pagamento deletado com sucesso", Payment = deletePayment });
            }
            catch (Exception ex)
            {
                await Log.LogToFile("log_", $"COD:1005-5 ,Erro interno ao deletar pagamento: {ex.Message}");
                return StatusCode(500, $"COD:1005-5 ,Erro interno do servidor");
            }
        }
    }
}