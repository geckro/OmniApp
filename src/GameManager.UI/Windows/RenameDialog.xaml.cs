using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using OmniApp.Common.Logging;
using System.Windows;

namespace GameManager.UI.Windows;

public partial class RenameDialog
{
    private readonly MetadataAccessor<Game> _gameAcc;
    private Game? _game;

    public RenameDialog(MetadataAccessor<Game> gameAcc)
    {
        InitializeComponent();
        _gameAcc = gameAcc;
    }

    public bool WasRenamed { get; private set; }

    public void SetCurrentGame(Game game)
    {
        _game = game;
        OldTitleLabel.Content = _game.Title;
    }

    private void RenameButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (_game != null)
        {
            _gameAcc.UpdatePropertyAndSave(_game.Id, "Title", NewTitleTextBox.Text);
            WasRenamed = true;
            Close();
        }
        else
        {
            Logger.Error(LogClass.GameMgrUi, "Cannot rename current game as game is null.");
        }
    }
}
