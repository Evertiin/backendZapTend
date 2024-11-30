using backend.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
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

                
                return Results.Ok(response);
            });
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.MapControllers();
            app.Run();
        }
    }
}
