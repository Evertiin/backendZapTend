using DBZapTend.Models;
using DBZapTend.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DBZapTend.Controllers
{
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
            var payment = await _repository.GetPayments();
            return Ok(payment);
        }

        [HttpPost]
        public async Task<ActionResult<Payment>> CreatePayment(Payment payment)
        {
            if (payment is null)
            {
                return BadRequest("Dados inválidos");
            }
            var createdPayment = await _repository.CreatePayment(payment);


            return Ok(createdPayment);
        }

        [HttpGet("{id:int}")]

        public async Task<ActionResult<Payment>> GetPaymenteId(int id)
        {
            var payment = await _repository.GetPayment(id);

            if (payment == null)
            {
                return NotFound("Pagamento não encontrado");
            }


            return Ok(payment);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Payment>> UpdatePayment(int id, Payment payment)
        {
            if (id != payment.IdPayments)
            {
                return BadRequest("Dados inválidos");
            }

            var updatePayment = await _repository.CreatePayment(payment);


            return Ok(updatePayment);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Payment>> DeletePayment(int id)
        {
            var payment = _repository.GetPayment(id);

            if (payment == null)
            {
                return NotFound("Pagamento não encontrada");
            }
            var deletePayment = await _repository.DeletePayment(id);


            return Ok(deletePayment);
        }
    }
}
