using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xacor.Ecs;
using Xacor.Graphics;
using Xacor.Graphics.Gl;

namespace Xacor;

public static class ApplicationEntryPoint
{
    public static void Run(
        string[] args,
        Action<IServiceCollection, IConfiguration> configureAdditionalServices)
    {
        using var serviceProvider = ConfigureServices(args, configureAdditionalServices);
        using var application = serviceProvider.GetRequiredService<IApplication>();
        
        application.Run();
    }

    private static ServiceProvider ConfigureServices(
        string[] args,
        Action<IServiceCollection, IConfiguration> configureAdditionalServices)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddJsonFile("appsettings.json", true)
            .AddCommandLine(args)
            .Build();
        
        var services = new ServiceCollection();
        services.AddSingleton(configuration);
        services.AddSingleton<IStatistics, Statistics>();
        services.AddSingleton<WindowHolder>();
        services.AddSingleton<IWindowGetter>(provider => provider.GetRequiredService<WindowHolder>());
        services.AddSingleton<IWindowSetter>(provider => provider.GetRequiredService<WindowHolder>());
        services.AddSingleton<CommandRecorder>();
        services.AddScoped<IGraphicsDevice, GlGraphicsDevice>();
        services.AddScoped<RenderSystem>();
        services.AddSingleton<Scene>(); // TODO(deccer) introduce some sort of SceneProvider which has an active scene or something like that
        services.AddSingleton<IApplication, Application>();
        configureAdditionalServices(services, configuration);
        
        return services.BuildServiceProvider();
    }
}