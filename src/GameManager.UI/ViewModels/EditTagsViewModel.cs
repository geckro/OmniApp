using GameManager.Core.Data.MetadataConstructors;
using GameManager.UI.Windows;
using OmniApp.Common.Logging;
using OmniApp.UI.Common;
using OmniApp.UI.Common.Helpers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GameManager.UI.ViewModels;

public class EditTagsViewModel : ViewModelBase
{
    private readonly WindowHelper _windowHelper;
    private Action? _closeEditEntryTagsWindow;

    public EditTagsViewModel(WindowHelper windowHelper)
    {
        _windowHelper = windowHelper;
        InitializeCommands();
    }

    public ICommand AddNewTagCommand { get; private set; } = null!;
    public ICommand RenameCurrentTagCommand { get; private set; } = null!;
    public ICommand DeleteCurrentTagOnGameCommand { get; private set; } = null!;
    public ICommand RenameTitleCommand { get; private set; } = null!;
    public Game? GameEntry { get; set; }

    private void InitializeCommands()
    {
        RenameTitleCommand = new RelayCommand<object>(_ => RenameTitle());
        AddNewTagCommand = new RelayCommand<object>(_ => AddNewTagToGame());
        RenameCurrentTagCommand = new RelayCommand<object>(_ => RenameTag());
        DeleteCurrentTagOnGameCommand = new RelayCommand<object>(_ => DeleteCurrentTag());
    }

    public void SetCloseAction(Action? closeAction)
    {
        _closeEditEntryTagsWindow = closeAction;
    }

    private void RenameTitle()
    {
        if (GameEntry == null)
        {
            Logger.Warning(LogClass.GameMgrUiViewModels, "Attempted to edit the title of a null game");
            return;
        }

        _windowHelper.ShowDialogWindow<RenameDialog>(window =>
        {
            if (window is RenameDialog renameDialog)
            {
                renameDialog.SetCurrentGame(GameEntry);

                Logger.Info(LogClass.GameMgrUiViewModels, $"{renameDialog.WasRenamed}");

                renameDialog.Closed += (_, _) =>
                {
                    _closeEditEntryTagsWindow?.Invoke();
                };
            }
            else
            {
                Logger.Error(LogClass.GameMgrUiViewModels, $"Expected RenameDialog window, got {window.GetType().Name}");
            }
        });
    }

    public static Dictionary<Label, TextBlock>? LoadTags(Game game)
    {
        Dictionary<Label, TextBlock> tagsDictionary = new();

        if (game.Tags == null || game.Tags.Count == 0)
        {
            return null;
        }

        foreach (KeyValuePair<string, ICollection<string>> tagPair in game.Tags)
        {
            Label keyLabel = new() { Content = tagPair.Key, FontWeight = FontWeights.Bold };

            TextBlock valueTextBlock = new()
            {
                    Text = string.Join(", ", tagPair.Value), VerticalAlignment = VerticalAlignment.Center
            };

            tagsDictionary.Add(keyLabel, valueTextBlock);
        }

        return tagsDictionary;
    }

    private void AddNewTagToGame()
    {
        _windowHelper.ShowDialogWindow<AddNewTagGameDialog>(window =>
        {
            if (window is AddNewTagGameDialog addNewTagGameWindow)
            {
                addNewTagGameWindow.SetCurrentGame(GameEntry);

                addNewTagGameWindow.Closed += (_, _) =>
                {
                    _closeEditEntryTagsWindow?.Invoke();
                };
            }
            else
            {
                Logger.Error(LogClass.GameMgrUiViewModels, $"Expected AddNewTagGameDialog window, got {window.GetType().Name}");
            }
        });
    }

    private void RenameTag()
    {
        throw new NotImplementedException();
    }

    private void DeleteCurrentTag()
    {
        throw new NotImplementedException();
    }
}
