using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xacor.Ecs;
using Xacor.Graphics;

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
        services.AddSingleton<GraphicsDeviceProvider>();
        services.AddSingleton<IGraphicsDeviceProvider>(provider => provider.GetRequiredService<GraphicsDeviceProvider>());
        services.AddSingleton<IGraphicsDeviceInitializer>(provider => provider.GetRequiredService<GraphicsDeviceProvider>());
        services.AddSingleton<RenderSystem>();
        services.AddSingleton<Scene>(); // TODO(deccer) introduce some sort of SceneProvider which has an active scene or something like that
        services.AddSingleton<IApplication, Application>();
        configureAdditionalServices(services, configuration);
        
        return services.BuildServiceProvider();
    }
}