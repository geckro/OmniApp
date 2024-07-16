﻿using GameManager.Core.Data;
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

        services.AddScoped<IMetadataAccessor<Game>>(sp =>
            new MetadataAccessor<Game>(
                sp.GetRequiredService<IMetadataPersistence>(),
                "games.json"
            )
        );
    }

    public App()
    {
        Logger.Debug(LogClass.OmniUi, "Starting Application");
    }
}
