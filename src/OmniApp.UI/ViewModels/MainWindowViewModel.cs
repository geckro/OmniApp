﻿using DietManager.UI.Windows;
using FinancialManager.UI.Windows;
using GameManager.UI.Windows;
using OmniApp.UI.Common;
using OmniApp.UI.Common.Helpers;
using System.Windows.Input;
using WindowsManager.UI.Windows;

namespace OmniApp.UI.ViewModels;

public class MainWindowViewModel
{
    private readonly WindowHelper _windowHelper;

    public MainWindowViewModel(WindowHelper windowHelper)
    {
        _windowHelper = windowHelper;
        OpenGameManagerCommand = new RelayCommand<object>(_ => OpenGameManager());
        OpenFinanceManagerCommand = new RelayCommand<object>(_ => OpenFinanceManager());
        OpenDietManagerCommand = new RelayCommand<object>(_ => OpenDietManager());
        OpenWindowsManagerCommand = new RelayCommand<object>(_ => OpenWindowsManager());
    }

    // ReSharper disable UnusedAutoPropertyAccessor.Global
    public ICommand OpenGameManagerCommand { get; }
    public ICommand OpenFinanceManagerCommand { get; }
    public ICommand OpenDietManagerCommand { get; }
    public ICommand OpenWindowsManagerCommand { get; }
    // ReSharper restore UnusedAutoPropertyAccessor.Global

    private void OpenGameManager()
    {
        _windowHelper.ShowDialogWindow<GameManagerWindow>();
    }

    private void OpenFinanceManager()
    {
        _windowHelper.ShowDialogWindow<FinanceManagerWindow>();
    }

    private void OpenDietManager()
    {
        _windowHelper.ShowDialogWindow<DietManagerWindow>();
    }

    private void OpenWindowsManager()
    {
        _windowHelper.ShowDialogWindow<WindowsManagerWindow>();
    }
}
