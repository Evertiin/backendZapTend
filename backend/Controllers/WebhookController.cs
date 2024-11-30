using Microsoft.AspNetCore.Mvc;

namespace WebhookApp.Controllers
{
    [ApiController]
    [Route("webhook")]
    public class WebhookController : ControllerBase
    {
        [HttpPost]
        public IActionResult ReceiveWebhook([FromBody] object payload)
        {
            // Log ou manipular o payload recebido
            Console.WriteLine($"{payload}");

            // Retorna uma resposta de sucesso
            return Ok(new { Message = "Webhook recebido com sucesso!" });
        }
    }
}
