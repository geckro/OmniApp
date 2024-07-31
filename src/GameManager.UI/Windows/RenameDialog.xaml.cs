using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using OmniApp.Common.Logging;
using System.Windows;

namespace GameManager.UI.Windows;

public partial class RenameDialog
{
    private Game? _game;
    private readonly MetadataAccessor<Game> _metadataAccessor;

    public bool WasRenamed { get; private set; }

    public RenameDialog(MetadataAccessor<Game> metadataAccessor)
    {
        InitializeComponent();
        _metadataAccessor = metadataAccessor;
    }

    public void SetCurrentGame(Game game)
    {
        _game = game;
        OldTitleLabel.Content = _game.Title;
    }

    private void RenameButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (_game != null)
        {
            _metadataAccessor.UpdateItemAndSave(_game.Id, "Title", NewTitleTextBox.Text);
            WasRenamed = true;
            Close();
        }
        else
        {
            Logger.Error(LogClass.GameMgrUi, "Cannot rename current game as game is null.");
        }
    }
}

