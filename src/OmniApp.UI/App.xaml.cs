using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using GameManager.UI.Helpers;
using GameManager.UI.ViewModels;
using GameManager.UI.Windows;
using Microsoft.Extensions.DependencyInjection;
using OmniApp.Common.Logging;
using System.Windows;

namespace OmniApp.UI;

public partial class App
{
    public App()
    {
        Logger.Debug(LogClass.OmniUi, "Starting Application");
    }

    public static IServiceProvider ServiceProvider { get; private set; } = null!;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        ServiceCollection serviceCollection = [];
        ConfigureServices(serviceCollection);
        ServiceProvider = serviceCollection.BuildServiceProvider();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<MetadataPersistence, MetadataPersistence>();
        services.AddSingleton<MetadataAccessorFactory, MetadataAccessorFactory>();

        services.AddTransient<AddGame>();
        services.AddTransient<EditEntry>();

        services.AddScoped<MainGameWindowViewModel>();
        services.AddScoped<DataGridHelper, DataGridHelper>();
        services.AddScoped<WindowHelper, WindowHelper>();
        services.AddScoped<FileHelper, FileHelper>();

        services.AddScoped<MetadataAccessor<Game>>(sp => new MetadataAccessor<Game>(sp.GetRequiredService<MetadataPersistence>(), "games.json"));
        services.AddScoped<MetadataAccessor<Genre>>(sp => new MetadataAccessor<Genre>(sp.GetRequiredService<MetadataPersistence>(), "genres.json"));
        services.AddScoped<MetadataAccessor<Developer>>(sp => new MetadataAccessor<Developer>(sp.GetRequiredService<MetadataPersistence>(), "developers.json"));
        services.AddScoped<MetadataAccessor<Publisher>>(sp => new MetadataAccessor<Publisher>(sp.GetRequiredService<MetadataPersistence>(), "publishers.json"));
        services.AddScoped<MetadataAccessor<Platform>>(sp => new MetadataAccessor<Platform>(sp.GetRequiredService<MetadataPersistence>(), "platforms.json"));
        services.AddScoped<MetadataAccessor<Series>>(sp => new MetadataAccessor<Series>(sp.GetRequiredService<MetadataPersistence>(), "series.json"));
    }
}
