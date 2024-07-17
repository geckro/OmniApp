using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using GameManager.UI.Helpers;
using GameManager.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using OmniApp.Common.Logging;
using System.Windows;

namespace OmniApp.UI;

public partial class App
{
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
        services.AddSingleton<IMetadataPersistence, MetadataPersistence>();
        services.AddSingleton<IMetadataAccessorFactory, MetadataAccessorFactory>();

        services.AddScoped<MainGameWindowViewModel>();
        services.AddScoped<IDataGridHelper, DataGridHelper>();
        services.AddScoped<IWindowHelper, WindowHelper>();

        services.AddScoped<IMetadataAccessor<Game>>(sp => new MetadataAccessor<Game>(sp.GetRequiredService<IMetadataPersistence>(), "games.json"));
        services.AddScoped<IMetadataAccessor<Genre>>(sp => new MetadataAccessor<Genre>( sp.GetRequiredService<IMetadataPersistence>(), "genres.json" ));
        services.AddScoped<IMetadataAccessor<Platform>>(sp => new MetadataAccessor<Platform>( sp.GetRequiredService<IMetadataPersistence>(), "platforms.json" ));
        services.AddScoped<IMetadataAccessor<Developer>>(sp => new MetadataAccessor<Developer>( sp.GetRequiredService<IMetadataPersistence>(), "developers.json" ));
        services.AddScoped<IMetadataAccessor<Publisher>>(sp => new MetadataAccessor<Publisher>( sp.GetRequiredService<IMetadataPersistence>(), "publisher.json" ));
        services.AddScoped<IMetadataAccessor<Series>>(sp => new MetadataAccessor<Series>( sp.GetRequiredService<IMetadataPersistence>(), "series.json" ));
    }

    public App()
    {
        Logger.Debug(LogClass.OmniUi, "Starting Application");
    }
}
