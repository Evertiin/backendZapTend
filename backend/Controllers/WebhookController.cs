using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;

namespace WebhookApp.Controllers
{
    [ApiController]
    [Route("webhook")]
    public class WebhookController : ControllerBase
    {
        public static WebhookEvent _lastPayload { get; set; }

        private readonly IHttpClientFactory _httpClientFactory;
        public WebhookController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task <IActionResult> ReceiveWebhook([FromBody] WebhookEvent payload)
        {
            _lastPayload = payload;

            //Console.WriteLine($"Evento: {payload.Data.Message.Conversation.ToString()}");
            var message = _lastPayload.Data.Message.Conversation;
            var number = _lastPayload.Data.Key.RemoteJid;
            var phone = ExtrairNumero(number);
            var contactName = _lastPayload.Data.PushName;

            HttpStatusCode status = await SendMessage(phone,message);
            
            return Ok(new { Message = "Webhook recebido com sucesso!" });
        }

        public static string ExtrairNumero(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("A string de entrada não pode ser nula ou vazia.");

            
            string[] partes = input.Split('@');
            return partes.Length > 0 ? partes[0] : string.Empty;
        }

        public async Task<HttpStatusCode> SendMessage(string number,string text)
        {
            var client = _httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Add("apikey", "wtwHLYfFxI9n1zDR8zFFqNq8kVaWqdD2oLpcjVmXBu");
            client.DefaultRequestHeaders.Add("User-Agent", "zaptend");


            var sendMessage = new SendMessage
            {
                number = number,
                text = text

            };
            string json = JsonConvert.SerializeObject(sendMessage);


            var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");


            var response = await client.PostAsync("https://evolutionzap.apievolution.shop/message/sendText/STAR", jsonContent);

            if (!response.IsSuccessStatusCode)
            {

                return response.StatusCode;
            }

            //JsonConvert.DeserializeObject<T>()
            var responseBody = await response.Content.ReadAsStringAsync();
            return response.StatusCode;
        }
    }

}

