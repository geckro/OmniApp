using DietManager.UI.ViewModels;
using DietManager.UI.Windows;
using FinancialManager.UI.ViewModels;
using FinancialManager.UI.Windows;
using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using GameManager.UI.Helpers;
using GameManager.UI.ViewModels;
using GameManager.UI.Windows;
using Microsoft.Extensions.DependencyInjection;
using OmniApp.Common.Data;
using OmniApp.Common.WindowsUtils;
using OmniApp.UI.Common.Helpers;
using OmniApp.UI.ViewModels;
using OmniApp.UI.Windows;

namespace OmniApp.UI;

/// <summary>
///     Class for configuring and using dependency injection services.
/// </summary>
public static class Services
{
    /// <summary>
    ///     Configures dependency injection services for the entire application.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to which the services will be added.</param>
    public static void ConfigureDepInjServices(IServiceCollection services)
    {
        AddSingletonServices(services);
        AddTransientServices(services);
        AddScopedServices(services);
    }

    /// <summary>
    ///     Adds scoped services to the <see cref="IServiceCollection" />.
    ///     Scoped services are created once per scope.
    /// </summary>
    /// <param name="sv">The <see cref="IServiceCollection" /> to which the services will be added.</param>
    private static void AddScopedServices(IServiceCollection sv)
    {
        // OmniApp
        sv.AddScoped<MainWindowViewModel>();
        sv.AddScoped<WindowHelper>();

        // GameManager
        sv.AddScoped<GameMgrWindowViewModel>();
        sv.AddScoped<ModifyMetadataViewModel>();
        sv.AddScoped<EditGameViewModel>();
        sv.AddScoped<AddGameViewModel>();
        sv.AddScoped<EditTagsViewModel>();
        sv.AddScoped<AddNewTagGameViewModel>();
        sv.AddScoped<AboutViewModel>();
        sv.AddScoped<PreferencesViewModel>();
        sv.AddScoped<PickGameViewModel>();
        sv.AddScoped<GameDateSetterViewModel>();
        sv.AddScoped<GameTableHelper>();
        sv.AddScoped<FileHelper>();
        sv.AddScoped<MetadataAccessor<Game>>(sp =>
                new MetadataAccessor<Game>(sp.GetRequiredService<MetadataPersistence>(), "games.json"));
        sv.AddScoped<MetadataAccessor<Genre>>(sp =>
                new MetadataAccessor<Genre>(sp.GetRequiredService<MetadataPersistence>(), "genres.json"));
        sv.AddScoped<MetadataAccessor<Developer>>(sp =>
                new MetadataAccessor<Developer>(sp.GetRequiredService<MetadataPersistence>(), "developers.json"));
        sv.AddScoped<MetadataAccessor<Publisher>>(sp =>
                new MetadataAccessor<Publisher>(sp.GetRequiredService<MetadataPersistence>(), "publishers.json"));
        sv.AddScoped<MetadataAccessor<Platform>>(sp =>
                new MetadataAccessor<Platform>(sp.GetRequiredService<MetadataPersistence>(), "platforms.json"));
        sv.AddScoped<MetadataAccessor<Series>>(sp =>
                new MetadataAccessor<Series>(sp.GetRequiredService<MetadataPersistence>(), "series.json"));

        // Finance Manager
        sv.AddScoped<FinanceManagerViewModel>();

        // Diet Manager
        sv.AddScoped<DietManagerViewModel>();
    }

    /// <summary>
    ///     Adds transient services to the <see cref="IServiceCollection" />.
    ///     Transient services are created each time they are requested.
    /// </summary>
    /// <param name="sv">The <see cref="IServiceCollection" /> to which the services will be added.</param>
    private static void AddTransientServices(IServiceCollection sv)
    {
        // OmniApp
        sv.AddTransient<MainWindow>();

        // GameManager
        sv.AddTransient<GameManagerWindow>();
        sv.AddTransient<ModifyMetadataWindow>();
        sv.AddTransient<PreferencesWindow>();
        sv.AddTransient<AboutDialog>();
        sv.AddTransient<AddGameWindow>();
        sv.AddTransient<GameDateSetterWindow>();
        sv.AddTransient<EditGameDialog>();
        sv.AddTransient<EditTagsDialog>();
        sv.AddTransient<RenameDialog>();
        sv.AddTransient<AddNewTagGameDialog>();
        sv.AddTransient<PickGameDialog>();

        // FinanceManager
        sv.AddTransient<FinanceManagerWindow>();

        // DietManager
        sv.AddTransient<DietManagerWindow>();
    }

    /// <summary>
    ///     Adds singleton services to the <see cref="IServiceCollection" />.
    ///     Singleton services are created once and shared through the entire application.
    /// </summary>
    /// <param name="sv">The <see cref="IServiceCollection" /> to which the services will be added.</param>
    private static void AddSingletonServices(IServiceCollection sv)
    {
        // GameManager
        sv.AddSingleton<MetadataPersistence>();
        sv.AddSingleton<MetadataAccessorFactory>();

        foreach (Type countryType in CountryTypeRegistry.CountryTypes)
        {
            sv.AddSingleton(typeof(ICountry), countryType);
        }

        sv.AddSingleton<CountryRegistry>();
    }
}
