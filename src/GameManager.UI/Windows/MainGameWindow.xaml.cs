﻿using GameManager.UI.Managers;
using GameManager.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using OmniApp.Common.Logging;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace GameManager.UI.Windows;

public partial class MainGameWindow
{
    private readonly MainGameWindowViewModel _viewModel;
    private readonly MainWindowContextMenuManager _contextMenuManager;

    public MainGameWindow(IServiceProvider serviceProvider)
    {
        Logger.Info(LogClass.GameMgrUi, "Starting MainGameWindow");

        _viewModel = serviceProvider.GetRequiredService<MainGameWindowViewModel>();

        InitializeComponent();
        DataContext = _viewModel;

        _contextMenuManager = new MainWindowContextMenuManager(this, _viewModel);
        _contextMenuManager.PopulateDataGridContextMenu();

        InitializeAsync();
    }

    private async void InitializeAsync()
    {
        try
        {
            Logger.Info(LogClass.GameMgrUi, "Initializing MainGameWindow");
            await _viewModel.InitializeAsync(GameDataGrid);
            RegisterKeyboardShortcuts();
        }
        catch (Exception ex)
        {
            Logger.Error(LogClass.GameMgrUi, $"Error initializing MainGameWindow: {ex.Message}");
        }
    }

    private void RegisterKeyboardShortcuts()
    {
        RoutedCommand command = new();
        command.InputGestures.Add(new KeyGesture(Key.N, ModifierKeys.Control));
        CommandBinding binding = new(command, (_, _) => AddButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent)));
        CommandBindings.Add(binding);
        InputBindings.Add(new InputBinding(command, new KeyGesture(Key.N, ModifierKeys.Control)));
    }
}