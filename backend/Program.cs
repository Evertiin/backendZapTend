using backend.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Text;
using WebhookApp.Controllers;

namespace backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddHttpClient();
            builder.Services.AddHttpClient();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });
            var app = builder.Build();
            app.UseStaticFiles();


            #region APIs Intancias
            //Endpoint pra criar instancia
            app.MapPost("api/createinstance", async (CreateInstanceDto dto, IHttpClientFactory httpClientFactory) =>
            {

                var client = httpClientFactory.CreateClient();


                client.DefaultRequestHeaders.Add("apikey", "wtwHLYfFxI9n1zDR8zFFqNq8kVaWqdD2oLpcjVmXBu");
                client.DefaultRequestHeaders.Add("User-Agent", "PixPaymentIntegrationTest");


                var postForm = new FormUrlEncodedContent(new[]
                {

                 new KeyValuePair<string, string>("instanceName", dto.instanceName),
                 new KeyValuePair<string, string>("qrCode", dto.qrCode.ToString()),
                 new KeyValuePair<string, string>("integration", dto.integration),

    });


                var response = await client.PostAsync("https://evolutionzap.apievolution.shop/instance/create", postForm);

                if (!response.IsSuccessStatusCode)
                {

                    return Results.BadRequest(response);
                }

                //JsonConvert.DeserializeObject<T>()
                var responseBody = await response.Content.ReadAsStringAsync();
                return Results.Ok(responseBody);
            });
            //Endpoint pra buscar código instancia
            app.MapGet("api/getcode/{instanceName}", async (string instanceName, IHttpClientFactory httpClientFactory, string number) =>
            {

                var client = httpClientFactory.CreateClient();


                client.DefaultRequestHeaders.Add("apikey", "wtwHLYfFxI9n1zDR8zFFqNq8kVaWqdD2oLpcjVmXBu");
                client.DefaultRequestHeaders.Add("User-Agent", "PixPaymentIntegrationTest");

                string url = $"https://evolutionzap.apievolution.shop/instance/connect/{instanceName}/?number={Uri.EscapeDataString(number)}";



                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {

                    return Results.BadRequest(response);
                }


                var responseBody = await response.Content.ReadAsStringAsync();
                return Results.Ok(responseBody);
            });
            //Endpoint pra deletar instancia
            app.MapDelete("api/deleteinstance/{instanceName}", async (string instanceName, IHttpClientFactory httpClientFactory) =>
            {

                var client = httpClientFactory.CreateClient();


                client.DefaultRequestHeaders.Add("apikey", "wtwHLYfFxI9n1zDR8zFFqNq8kVaWqdD2oLpcjVmXBu");
                client.DefaultRequestHeaders.Add("User-Agent", "PixPaymentIntegrationTest");

                string url = $"https://evolutionzap.apievolution.shop/instance/delete/{instanceName}";

                var response = await client.DeleteAsync(url);

                if (!response.IsSuccessStatusCode)
                {

                    return Results.BadRequest(response);
                }


                var responseBody = await response.Content.ReadAsStringAsync();
                return Results.Ok(responseBody);
            });

            #endregion

            #region API Webhook
            //Endpoint pra ativar webhook
            app.MapPost("api/webhookactive/{instanceName}", async (string instanceName, IHttpClientFactory httpClientFactory) =>
            {

                var client = httpClientFactory.CreateClient();


                client.DefaultRequestHeaders.Add("apikey", "wtwHLYfFxI9n1zDR8zFFqNq8kVaWqdD2oLpcjVmXBu");
                client.DefaultRequestHeaders.Add("User-Agent", "PixPaymentIntegrationTest");


                var webhookRequest = new WebhookRequest
                {
                    webhook = new webhook
                    {
                        enabled = true,
                        url = "https://webhook.site",
                        events = new List<string> { "MESSAGES_UPSERT" }
                    }
                };
                string json = JsonConvert.SerializeObject(webhookRequest);

                
                var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"https://evolutionzap.apievolution.shop/webhook/set/{instanceName}", jsonContent);

                if (!response.IsSuccessStatusCode)
                { 
                    return Results.BadRequest(response); }
                 
                var responseBody = await response.Content.ReadAsStringAsync();

                return Results.Ok(responseBody);
            });

            #endregion

            //app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.MapControllers();
            app.Run();
        }
    }
}
