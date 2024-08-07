using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using GameManager.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using OmniApp.Common.Logging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GameManager.UI.Windows;

public partial class AddNewTagGameDialog
{
    private readonly MetadataAccessor<Game> _gameAcc;
    private readonly AddNewTagGameViewModel _viewModel;
    private Game? _game;

    public AddNewTagGameDialog(IServiceProvider sp, MetadataAccessor<Game> gameAcc)
    {
        _gameAcc = gameAcc;

        _viewModel = sp.GetRequiredService<AddNewTagGameViewModel>();
        DataContext = _viewModel;
        InitializeComponent();
    }

    public bool TagAddedToGame { get; private set; }

    public void SetCurrentGame(Game? game)
    {
        _game = game;
    }

    private void AddNewTag_OnClick(object sender, RoutedEventArgs e)
    {
        if (_game != null)
        {
            TagTypes? getTagType = GetTagType();

            switch (getTagType)
            {
                case null:
                    Logger.Error(LogClass.GameMgrUiWindows, "No tag type.");
                    Close();
                    break;
                case TagTypes.Boolean:
                    AddBooleanTag();
                    break;
                case TagTypes.String:
                    AddTextTag();
                    break;
                case TagTypes.Game:
                    AddGameTag();
                    break;
                default:
                    throw new Exception("AddNewTagGameDialog: How did you get here");
            }
        }
        else
        {
            Logger.Error(LogClass.GameMgrUiWindows, "Cannot add tag to current game as game is null.");
        }
    }

    private void AddBooleanTag()
    {
        Dictionary<string, ICollection<string>> tagToAdd = [];
        ICollection<string> values = [];
        if (YesCheckBox.IsChecked == true)
        {
            values.Add("bool:y");
        }
        else if (NoCheckBox.IsChecked == true)
        {
            values.Add("bool:n");
        }

        tagToAdd.Add(TagKeyTextBox.Text.Trim(), values);
        AddTagToMetadata(tagToAdd);
    }

    private void AddTextTag()
    {
        Dictionary<string, ICollection<string>> tagToAdd = [];
        ICollection<string> values = [];

        string key = TagKeyTextBox.Text.Trim();
        string value = TagValuesTextBox.Text.Trim();

        string[] splitValues = value.Split(';');
        foreach (string splitValue in splitValues)
        {
            string trimmedValue = splitValue.Trim();
            if (!string.IsNullOrEmpty(trimmedValue))
            {
                values.Add(trimmedValue);
            }
        }

        tagToAdd.Add(key, values);

        AddTagToMetadata(tagToAdd);
    }

    private void AddGameTag()
    {
        Dictionary<string, ICollection<string>> tagToAdd = [];
        ICollection<string> values = [];
        if (_viewModel.GameId != "")
        {
            values.Add($"game:{_viewModel.GameId}");
        }

        tagToAdd.Add(TagKeyTextBox.Text.Trim(), values);
        AddTagToMetadata(tagToAdd);
    }

    private void AddTagToMetadata(Dictionary<string, ICollection<string>> tagToAdd)
    {
        _gameAcc.AddTagToMetadata(_game!, tagToAdd);
        TagAddedToGame = true;
        Close();
    }

    private TagTypes? GetTagType()
    {
        if (YesCheckBox.IsChecked == true || NoCheckBox.IsChecked == true)
        {
            return TagTypes.Boolean;
        }

        if (TagValuesTextBox.Text.Trim() != "")
        {
            return TagTypes.String;
        }

        if (_viewModel.GameId != null)
        {
            return TagTypes.Game;
        }

        return null;
    }

    private void CheckBox_IsEitherChecked(object sender, RoutedEventArgs e)
    {
        if (YesCheckBox.IsChecked == true || NoCheckBox.IsChecked == true)
        {
            TagValuesTextBox.Opacity = 0.5d;
            TagValuesTextBox.IsReadOnly = true;
            TagValuesTextBox.Cursor = Cursors.No;
        }
        else
        {
            TagValuesTextBox.Opacity = 1d;
            TagValuesTextBox.IsReadOnly = false;
            TagValuesTextBox.Cursor = Cursors.IBeam;
        }
    }

    private void TagValuesTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (TagValuesTextBox.Text.Trim() != "")
        {
            YesCheckBox.IsEnabled = false;
            NoCheckBox.IsEnabled = false;
        }
        else
        {
            YesCheckBox.IsEnabled = true;
            NoCheckBox.IsEnabled = true;
        }
    }
}

public enum TagTypes
{
    Boolean,
    String,
    Game
}
