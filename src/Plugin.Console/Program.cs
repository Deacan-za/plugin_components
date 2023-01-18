using Interfaces.Lib;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Plugin.ConsoleApp;
using Plugin.ConsoleApp.Options;
using System;
using System.Threading.Tasks;

_ = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var configurationRoot = context.Configuration;
        services
            .Configure<ModeOption>(configurationRoot.GetSection(nameof(ModeOption)))
            .AddTransient<IHandlerFactory, HandlerFactory>()
            .AddSingleton<IPlugInProvider, PlugInProvider>();
    })
    .Build();

await RegisterPluginsAsync(host.Services);
await RequestOneAsync(host.Services);
await RequestTwoAsync(host.Services);

await host.RunAsync();

static async Task RegisterPluginsAsync(IServiceProvider services)
{
    using var serviceScope = services.CreateScope();
    var serviceProvider = serviceScope.ServiceProvider;

    var plugInProvider = serviceProvider.GetService<IPlugInProvider>();
    
    plugInProvider.RegisterPlugIns();

    await Task.CompletedTask;
}

static async Task RequestOneAsync(IServiceProvider services)
{
    const int requestMode = 1;
    using var serviceScope = services.CreateScope();
    var serviceProvider = serviceScope.ServiceProvider;

    var plugInProvider = serviceProvider.GetService<IPlugInProvider>();

    var handler = plugInProvider.GetHandler(requestMode);

    handler.ProcessRequest();

    await Task.CompletedTask;
}

static async Task RequestTwoAsync(IServiceProvider services)
{
    const int requestMode = 2;
    using var serviceScope = services.CreateScope();
    var serviceProvider = serviceScope.ServiceProvider;

    var plugInProvider = serviceProvider.GetService<IPlugInProvider>();

    var handler = plugInProvider.GetHandler(requestMode);

    handler.ProcessRequest();

    await Task.CompletedTask;
}