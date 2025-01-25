using APIWhatssApp.Models;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
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
        public static ReceiveFlowiseMessage receive { get; set; }
        public static string _phone;
        public static string _number;
        public static string _instance;
        private readonly IConfiguration _configuration;

        private readonly IHttpClientFactory _httpClientFactory;
        public WebhookController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> ReceiveWebhook([FromBody] WebhookEvent payload)
        {
             _lastPayload = payload;

            //Console.WriteLine($"Evento: {payload.Data.Message.Conversation.ToString()}");
            var message = _lastPayload.Data.Message.Conversation;
            _number = _lastPayload.Data.Key.RemoteJid;
            var _phone = ExtrairNumero(_number);
            _instance = _lastPayload.Instance;

           // string url = $"https://evolutionzap.apievolution.shop/instance/connect/{instanceName}/?number={Uri.EscapeDataString(number)}";


            
            var status =  await SendMessageFlowise(_phone,message);
            

            return Ok();
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


            var response = await client.PostAsync($"https://apizap.staranytech.fun/message/sendText/{_instance}", jsonContent);

            if (!response.IsSuccessStatusCode)
            {

                return response.StatusCode;
            }

            //JsonConvert.DeserializeObject<T>()
            var responseBody = await response.Content.ReadAsStringAsync();
            return response.StatusCode;
        }
        [HttpPost]
        [Route("api")]
        public async Task <HttpStatusCode> ModifyPrompt(string prompt,string chatId)
            //,string nameChat,string idChatFlow)
        {
            var client = _httpClientFactory.CreateClient();
            //client.DefaultRequestHeaders.Add("Authorization", "Bearer dZtUF5z6p4caAoapNrnHiLrGaTV6z2zvQY207nIK85M");
            var response = await client.GetAsync($"https://flowise.staranytech.fun/api/v1/chatflows/{chatId}");
            var chatFlow = _configuration.GetSection("ChatFlow").Get<ChatFlow>();
            JObject flowData = JObject.Parse(chatFlow.FlowData);
            var idChat = flowData["id"]?.ToString(); // Acessa o Id
            var chatName = flowData["name"]?.ToString(); // Acessa o Name

            // Exemplo de modificação do Id e Name
            //flowData["id"] = idChatFlow; 
            //flowData["name"] = nameChat;
            //var responseBody = await response.Content.ReadAsStringAsync();
            //JObject jsonObject = JObject.Parse(chatFlow);
            //JObject dataFlow = JObject.Parse(flowData["flowData"].ToString());

            if (flowData["nodes"] is JArray nodes)
            {
                foreach (var node in nodes)
                {
                    
                    if ((string)node["id"] == "conversationalAgent_0")
                    {
                        
                        if (node["data"]?["inputs"]?["systemMessage"] != null)
                        {
                            
                            node["data"]["inputs"]["systemMessage"] = prompt;
                            break;
                        }
                        else
                        {
                            return response.StatusCode;
                        }
                    }
                }
            }
            else
            {
                return response.StatusCode;
            }

            flowData["flowData"] = flowData.ToString(Formatting.None);

            // Serializar o JSON atualizado de volta para uma string
            string jsonAtualizado = flowData.ToString(Formatting.Indented);
            // Serializar de volta para JSON formatado
            return response.StatusCode;

        }
        public async Task<string> SendMessageFlowise(string _phone,string message)
        {
            var client = _httpClientFactory.CreateClient();

            //client.DefaultRequestHeaders.Add("apikey", "wtwHLYfFxI9n1zDR8zFFqNq8kVaWqdD2oLpcjVmXBu");
            client.DefaultRequestHeaders.Add("User-Agent", "zaptend");

            var idSession = _number;

          
            var messageChat = new SendFlowiseMessage
            {
               question = message,
                overrideConfig = new overrideConfig
                {
                    sessionId = idSession
                }

            };
            string json = JsonConvert.SerializeObject(messageChat);


            var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");


            var response = await client.PostAsync($"https://flowise.staranytech.fun/api/v1/prediction/{_instance}", jsonContent);

           
            var responseBody = await response.Content.ReadAsStringAsync();
            var messageText = JsonConvert.DeserializeObject<ReceiveFlowiseMessage>(responseBody);

            var Text = messageText.text;
            //var id = messageText.sessionId;

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

            return JsonConvert.SerializeObject(new
            {
                sessionId = idSession,
                response = responseBody
            });

        }

    }
    public class ChatFlow
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string FlowData { get; set; }
        public bool Deployed { get; set; }
        public bool IsPublic { get; set; }
        public string ApiKeyId { get; set; }
        public string ChatbotConfig { get; set; }
        public string ApiConfig { get; set; }
        public string Analytic { get; set; }
        public string SpeechToText { get; set; }
        public string FollowUpPrompts { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }

}

