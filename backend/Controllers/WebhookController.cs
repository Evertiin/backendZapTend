using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace WebhookApp.Controllers
{
    [ApiController]
    [Route("webhook")]
    public class WebhookController : ControllerBase
    {
        public static WebhookEvent _lastPayload { get; set; }

        [HttpPost]
        public IActionResult ReceiveWebhook([FromBody] WebhookEvent payload)
        {
            _lastPayload = payload;

           // Console.WriteLine($"Evento: {payload.ToString()}");


            return Ok(new { Message = "Webhook recebido com sucesso!" });
        }
    }
}
