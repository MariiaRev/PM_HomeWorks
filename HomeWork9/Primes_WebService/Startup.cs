using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;
using Primes_WebService.Services;
using System.Net;
using System.Text;

namespace Primes_WebService
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IPrimesService, PrimesService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    var loggingInfo = new StringBuilder($"Request: {context.Request.Path}\n");
                    var title = new StringBuilder()
                                .Append($"{"", 5}This is Homework 9 Task 1. ")
                                .Append($"Prime Numbers Web Service\n\n")
                                .Append($"{"", 20}Made by Mariia Revenko");

                    await context.Response.WriteAsync(title.ToString());

                    var logger = context.RequestServices.GetRequiredService<ILogger<Startup>>();
                    loggingInfo.Append($"Expected http status code: {(int)HttpStatusCode.OK}\n")
                               .Append($"Result (http status code): {context.Response.StatusCode}");

                    logger.LogInformation(loggingInfo.ToString());
                });

                endpoints.MapGet("/primes/{number:int}", async context =>
                {
                    var number = int.Parse((string)context.Request.RouteValues["number"]);
                    var primesService = context.RequestServices.GetRequiredService<IPrimesService>();
                    var logger = context.RequestServices.GetRequiredService<ILogger<Startup>>();
                    var isPrime = await primesService.IsPrimeAsync(number);
                    var loggingInfo = new StringBuilder($"Request: {context.Request.Path}\n");

                    if (isPrime)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.OK;
                        await context.Response.WriteAsync($"The number {number} is prime.");

                        loggingInfo.Append($"Expected message: The number {number} is prime.\n")
                                  .Append($"Expected http status code: {(int)HttpStatusCode.OK}\n");
                    }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        await context.Response.WriteAsync($"The number {number} is not prime.");

                        loggingInfo.Append($"Expected message: The number {number} is not prime.\n")
                                   .Append($"Expected http status code: {(int)HttpStatusCode.NotFound}\n");
                    }

                    loggingInfo.Append($"Result (http status code): {context.Response.StatusCode}");
                    logger.LogInformation(loggingInfo.ToString());
                });

                // /primes?from=x&to=y
                endpoints.MapGet("/primes", async context =>
                {
                    var primesService = context.RequestServices.GetRequiredService<IPrimesService>();
                    var logger = context.RequestServices.GetRequiredService<ILogger<Startup>>();
                    var fromString = context.Request.Query["from"].FirstOrDefault();
                    var toString = context.Request.Query["to"].FirstOrDefault();
                    var loggingInfo = new StringBuilder($"Request: {context.Request.Path}{context.Request.QueryString}\n");

                    if (int.TryParse(fromString, out var from) && int.TryParse(toString, out var to))
                    {
                        var primes = await primesService.GetPrimesInRangeAsync(from, to);
                        var responseContent = $"[{string.Join(", ", primes)}]";

                        await context.Response.WriteAsync(responseContent);

                        loggingInfo.Append($"Content: \n{responseContent}\n");
                        loggingInfo.Append($"Expected http status code: {(int)HttpStatusCode.OK}\n");
                    }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        loggingInfo.Append($"Expected http status code: {(int)HttpStatusCode.BadRequest}\n");
                    }

                    loggingInfo.Append($"Result (http status code): {context.Response.StatusCode}");
                    logger.LogInformation(loggingInfo.ToString());
                });
            });
        }
    }
}
