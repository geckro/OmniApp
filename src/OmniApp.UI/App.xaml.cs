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

        services.AddTransient<AddGame>();

        services.AddScoped<MainGameWindowViewModel>();
        services.AddScoped<IDataGridHelper, DataGridHelper>();
        services.AddScoped<IWindowHelper, WindowHelper>();

        services.AddScoped<IMetadataAccessor<AgeRatings>>(sp => new MetadataAccessor<AgeRatings>(sp.GetRequiredService<IMetadataPersistence>(), "ageratings.json"));
        services.AddScoped<IMetadataAccessor<Artist>>(sp => new MetadataAccessor<Artist>(sp.GetRequiredService<IMetadataPersistence>(), "artists.json"));
        services.AddScoped<IMetadataAccessor<Composer>>(sp => new MetadataAccessor<Composer>(sp.GetRequiredService<IMetadataPersistence>(), "composers.json"));
        services.AddScoped<IMetadataAccessor<Designer>>(sp => new MetadataAccessor<Designer>(sp.GetRequiredService<IMetadataPersistence>(), "designers.json"));
        services.AddScoped<IMetadataAccessor<Developer>>(sp => new MetadataAccessor<Developer>( sp.GetRequiredService<IMetadataPersistence>(), "developers.json" ));
        services.AddScoped<IMetadataAccessor<Director>>(sp => new MetadataAccessor<Director>(sp.GetRequiredService<IMetadataPersistence>(), "directors.json"));
        services.AddScoped<IMetadataAccessor<Engine>>(sp => new MetadataAccessor<Engine>(sp.GetRequiredService<IMetadataPersistence>(), "engines.json"));
        services.AddScoped<IMetadataAccessor<Game>>(sp => new MetadataAccessor<Game>(sp.GetRequiredService<IMetadataPersistence>(), "games.json"));
        services.AddScoped<IMetadataAccessor<Genre>>(sp => new MetadataAccessor<Genre>(sp.GetRequiredService<IMetadataPersistence>(), "genres.json"));
        services.AddScoped<IMetadataAccessor<Platform>>(sp => new MetadataAccessor<Platform>( sp.GetRequiredService<IMetadataPersistence>(), "platforms.json" ));
        services.AddScoped<IMetadataAccessor<Producer>>(sp => new MetadataAccessor<Producer>(sp.GetRequiredService<IMetadataPersistence>(), "producers.json"));
        services.AddScoped<IMetadataAccessor<Programmer>>(sp => new MetadataAccessor<Programmer>( sp.GetRequiredService<IMetadataPersistence>(), "programmers.json" ));
        services.AddScoped<IMetadataAccessor<Publisher>>(sp => new MetadataAccessor<Publisher>( sp.GetRequiredService<IMetadataPersistence>(), "publishers.json" ));
        services.AddScoped<IMetadataAccessor<Series>>(sp => new MetadataAccessor<Series>( sp.GetRequiredService<IMetadataPersistence>(), "series.json" ));
        services.AddScoped<IMetadataAccessor<Writer>>(sp => new MetadataAccessor<Writer>( sp.GetRequiredService<IMetadataPersistence>(), "writers.json" ));
    }

    public App()
    {
        Logger.Debug(LogClass.OmniUi, "Starting Application");
    }
}
