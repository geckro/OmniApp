using GameManager.Core.Data;
using GameManager.Core.Data.MetadataConstructors;
using OmniApp.Common.Logging;
using System.Windows;

namespace GameManager.UI.Windows;

public partial class AddNewTagGameDialog
{
    private readonly MetadataAccessor<Game> _metadataAccessor;
    private Game? _game;

    public AddNewTagGameDialog(MetadataAccessor<Game> metadataAccessor)
    {
        _metadataAccessor = metadataAccessor;
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

            _metadataAccessor.AddTagToMetadata(_game, tagToAdd);
            TagAddedToGame = true;
            Close();
        }
        else
        {
            Logger.Error(LogClass.GameMgrUi, "Cannot add tag to current game as game is null.");
        }
    }
}
