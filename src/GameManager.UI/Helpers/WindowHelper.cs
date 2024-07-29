﻿using Microsoft.Extensions.DependencyInjection;
using OmniApp.Common.Logging;
using System.Windows;

namespace GameManager.UI.Helpers;

public class WindowHelper(IServiceProvider serviceProvider)
{
    private readonly IServiceProvider _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

    /// <summary>
    ///     Shows the window.
    /// </summary>
    public T ShowWindow<T>() where T : Window
    {
        Logger.Debug(LogClass.GameMgrUi, $"ShowWindow<{typeof(T).Name}> called");
        T window = _serviceProvider.GetRequiredService<T>();
        window.Show();
        return window;
    }
}
