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
        public static object _lastPayload;

        [HttpPost]
        public IActionResult ReceiveWebhook([FromBody] object payload)
        {
            _lastPayload = payload;

            Console.WriteLine($"{_lastPayload}");


            return Ok(new { Message = "Webhook recebido com sucesso!" });
        }
    }
}
