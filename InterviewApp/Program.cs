using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using InterviewApp.Services;
using InterviewApp.Models;
using MediatR;
using InterviewApp.Queries;
using System;

class Program
{
    static async Task Main(string[] args)
    {
        using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((builder, services) => {

                services.AddLogging();
                services.Configure<LanguageSetting>(options => {
                    builder.Configuration.GetSection("LangSettings").Bind(options);
                });
                services.AddTransient<ITimeGreetingService, TimeGreetingService>();
                services.AddTransient<IGreetingService, GreetingService>();
                services.AddMediatR(typeof(Program).Assembly);
            
            })
            .Build();

        var mediator = host.Services.GetRequiredService<IMediator>();
        var greetinging = await mediator.Send(new GreetUserQuery());
        Console.WriteLine($"{nameof(GreetUserQuery)} results:\n{greetinging}\n");
        Console.WriteLine(".................................................\n");
        Console.WriteLine($"{nameof(GetTimeGreetingQuery)} results:");
        var timeGreeting = await mediator.Send(new GetTimeGreetingQuery());
        
        await host.RunAsync();
    }
}