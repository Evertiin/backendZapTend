using APIWhatssApp.Models;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Numerics;
using System.Text;
using System.Xml.Schema;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebhookApp.Controllers
{
    [ApiController]
    [Route("webhook")]
    public class WebhookController : ControllerBase
    {
        public static  WebhookEvent _lastPayload { get; set; }
        //public static ReceiveFlowiseMessage receive { get; set; }
        public static string _phone;

        private readonly IHttpClientFactory _httpClientFactory;
        public WebhookController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> ReceiveWebhook([FromBody] WebhookEvent payload)
        {
             _lastPayload = payload;

            //Console.WriteLine($"Evento: {payload.Data.Message.Conversation.ToString()}");
            var message = _lastPayload.Data.Message.Conversation;
            var number = _lastPayload.Data.Key.RemoteJid;
            var _phone = ExtrairNumero(number);
            var contactName = _lastPayload.Data.PushName;

            var status =  await SendMessageFlowise(_phone,message);
            //HttpStatusCode statuss = await SendMessage(phone, message);

            return Ok(new { Message = "Webhook recebido com sucesso!" });
        }

        public static string ExtrairNumero(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("A string de entrada não pode ser nula ou vazia.");


            string[] partes = input.Split('@');
            return partes.Length > 0 ? partes[0] : string.Empty;
        }

        public async Task<HttpStatusCode> SendMessage(string number, string text)
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


            var response = await client.PostAsync("https://apizap.staranytech.fun/message/sendText/star", jsonContent);

            if (!response.IsSuccessStatusCode)
            {

                return response.StatusCode;
            }

            //JsonConvert.DeserializeObject<T>()
            var responseBody = await response.Content.ReadAsStringAsync();
            return response.StatusCode;
        }


        public async Task<string> SendMessageFlowise(string _phone,string message)
        {
            var client = _httpClientFactory.CreateClient();

            //client.DefaultRequestHeaders.Add("apikey", "wtwHLYfFxI9n1zDR8zFFqNq8kVaWqdD2oLpcjVmXBu");
            client.DefaultRequestHeaders.Add("User-Agent", "zaptend");


            var messageChat = new SendFlowiseMessage
            {
               question = message,

            };
            string json = JsonConvert.SerializeObject(messageChat);


            var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");


            var response = await client.PostAsync("https://flowise.staranytech.fun/api/v1/prediction/5a62edf6-3312-4567-a30c-37349592c069", jsonContent);

           
            var responseBody = await response.Content.ReadAsStringAsync();
            var messageText = JsonConvert.DeserializeObject<ReceiveFlowiseMessage>(responseBody);

            var Text = messageText.text;
            var id = messageText.sessionId;

            var sendMessage = new SendMessage
            {
                number = _phone,
                text = Text

            };

            string jsonn = JsonConvert.SerializeObject(sendMessage);


            var jsonContentt = new StringContent(json, Encoding.UTF8, "application/json");


            //var responsee = await client.PostAsync("https://apizap.staranytech.fun/message/sendText/star", jsonContent);
            HttpStatusCode statuss = await SendMessage(_phone, Text);



            //JsonConvert.DeserializeObject<T>()
            var responseBodyy = await response.Content.ReadAsStringAsync();

            return responseBody;

        }

    }
}

