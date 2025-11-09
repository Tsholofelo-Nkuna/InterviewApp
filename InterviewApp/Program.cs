using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using InterviewApp.Services;
using InterviewApp.Models;
using MediatR;
using InterviewApp.Queries;

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
                services.Configure<LanugageSetting>(options => {
                    builder.Configuration.GetSection("LangSettings").Bind(options);
                });
                services.AddTransient<ITimeGreetingService, TimeGreetingService>();
                services.AddTransient<IGreetingService, GreetingService>();
                services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<Program>());
            
            })
            .Build();

        var mediator = host.Services.GetRequiredService<IMediator>();
        await mediator.Send(new GreetUserQuery());

        await host.RunAsync();
    }
}