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
                var payment = await _repository.GetPayments();
                return Ok(payment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao buscar pagamentos: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Payment>> CreatePayment(Payment payment)
        {
            try
            {
                if (payment is null)
                {
                    return BadRequest("Dados inválidos");
                }

                var createdPayment = await _repository.CreatePayment(payment);
                return Ok(createdPayment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao criar pagamento: {ex.Message}");
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
                    return NotFound("Pagamento não encontrado");
                }

                return Ok(payment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao buscar pagamento por ID: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Payment>> UpdatePayment(int id, Payment payment)
        {
            try
            {
                if (id != payment.IdPayments)
                {
                    return BadRequest("Dados inválidos");
                }

                var updatePayment = await _repository.UpdatePayment(payment);
                return Ok(updatePayment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao atualizar pagamento: {ex.Message}");
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
                    return NotFound("Pagamento não encontrado");
                }

                var deletePayment = await _repository.DeletePayment(id);
                return Ok(deletePayment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao deletar pagamento: {ex.Message}");
            }
        }
    }
}