﻿// using GameManager.Core.Data;
// using GameManager.Core.Data.MetadataConstructors;
// using GameManager.UI.Windows;
// using System.Reflection;
// using System.Windows;
// using System.Windows.Controls;
// using System.Windows.Input;
//
// namespace GameManager.UI.Managers;
//
// public class AddGameMetadataManager(AddGame addGame, IMetadataAccessorFactory iMetadataAccessorFactory)
// {
//     private readonly Dictionary<string, bool> _checkedStates = new();
//
//     /// <summary>
//     ///     Initializes the metadata areas for various Game categories.
//     /// </summary>
//     public void InitializeMetadataAreas()
//     {
//         MakeMetadataAreas("Genre", addGame.GenreStackPanel, iMetadataAccessorFactory.CreateMetadataAccessor<Genre>().LoadMetadataCollection());
//         MakeMetadataAreas("Platform", addGame.PlatformStackPanel, iMetadataAccessorFactory.CreateMetadataAccessor<Platform>().LoadMetadataCollection());
//         MakeMetadataAreas("Developer", addGame.DeveloperStackPanel, iMetadataAccessorFactory.CreateMetadataAccessor<Developer>().LoadMetadataCollection());
//         MakeMetadataAreas("Publisher", addGame.PublisherStackPanel, iMetadataAccessorFactory.CreateMetadataAccessor<Publisher>().LoadMetadataCollection());
//         MakeMetadataAreas("Series", addGame.SeriesStackPanel, iMetadataAccessorFactory.CreateMetadataAccessor<Series>().LoadMetadataCollection());
//         MakeMetadataAreas("Composer", addGame.ComposerStackPanel, iMetadataAccessorFactory.CreateMetadataAccessor<Composer>().LoadMetadataCollection());
//         MakeMetadataAreas("Director", addGame.DirectorStackPanel, iMetadataAccessorFactory.CreateMetadataAccessor<Director>().LoadMetadataCollection());
//         MakeMetadataAreas("Engine", addGame.EngineStackPanel, iMetadataAccessorFactory.CreateMetadataAccessor<Engine>().LoadMetadataCollection());
//         MakeMetadataAreas("Writer", addGame.WriterStackPanel, iMetadataAccessorFactory.CreateMetadataAccessor<Writer>().LoadMetadataCollection());
//         MakeMetadataAreas("AgeRating", addGame.AgeRatingStackPanel, iMetadataAccessorFactory.CreateMetadataAccessor<AgeRatings>().LoadMetadataCollection());
//         MakeMetadataAreas("Producer", addGame.ProducerStackPanel, iMetadataAccessorFactory.CreateMetadataAccessor<Producer>().LoadMetadataCollection());
//         MakeMetadataAreas("Designer", addGame.DesignerStackPanel, iMetadataAccessorFactory.CreateMetadataAccessor<Designer>().LoadMetadataCollection());
//         MakeMetadataAreas("Programmer", addGame.ProgrammerStackPanel, iMetadataAccessorFactory.CreateMetadataAccessor<Programmer>().LoadMetadataCollection());
//         MakeMetadataAreas("Artist", addGame.ArtistStackPanel, iMetadataAccessorFactory.CreateMetadataAccessor<Artist>().LoadMetadataCollection());
//     }
//     /// <summary>
//     ///     Creates metadata areas for a specific category.
//     /// </summary>
//     /// <param name="name">The name of the metadata category.</param>
//     /// <param name="stackPanel">The stack panel to add the generated controls to.</param>
//     /// <param name="dataSource">The actual data source of the metadata.</param>
//     /// <typeparam name="T">The type of IMetadata.</typeparam>
//     private void MakeMetadataAreas<T>(string name, StackPanel stackPanel, ICollection<T> dataSource) where T : IMetadata
//     {
//         Label label = new() { Content = name };
//
//         TextBox textBox = new() { Name = $"{name}TextBox" };
//         addGame.RegisterName(textBox.Name, textBox);
//         textBox.TextChanged += (sender, _) => TextBox_TextChanged(sender, dataSource);
//         textBox.KeyDown += (sender, e) => TextBox_KeyDown(sender, e, typeof(T));
//
//         ListBox listBox = new()
//         {
//             Name = $"{name}ListBox",
//             MaxHeight = 250,
//             Visibility = Visibility.Collapsed,
//             Padding = new Thickness(0),
//             Margin = new Thickness(0),
//             FontSize = 14,
//             ItemContainerStyle = CreateListBoxItemStyle()
//         };
//         addGame.RegisterName(listBox.Name, listBox);
//
//         stackPanel.Children.Add(label);
//         stackPanel.Children.Add(textBox);
//         stackPanel.Children.Add(listBox);
//     }
//
//     /// <summary>
//     ///     Creates a style for the list box item for the metadata areas.
//     /// </summary>
//     /// <returns>The created list box item style.</returns>
//     private static Style CreateListBoxItemStyle()
//     {
//         Style style = new(typeof(ListBoxItem));
//         style.Setters.Add(new Setter(Control.PaddingProperty, new Thickness(2)));
//         style.Setters.Add(new Setter(Control.MarginProperty, new Thickness(0)));
//
//         return style;
//     }
//
//     /// <summary>
//     ///     Handles the TextChanged event of the current text box.
//     /// </summary>
//     /// <param name="sender">The source of the event.</param>
//     /// <param name="suggestions">The collection of suggestions to show.</param>
//     /// <typeparam name="T">The type of IMetadata.</typeparam>
//     private void TextBox_TextChanged<T>(object sender, ICollection<T> suggestions) where T : IMetadata
//     {
//         if (sender is not TextBox textBox)
//         {
//             return;
//         }
//
//         string listBoxName = textBox.Name.Replace("TextBox", "ListBox");
//         if (addGame.FindName(listBoxName) is not ListBox listBox)
//         {
//             return;
//         }
//
//         string text = textBox.Text.Trim();
//
//         if (string.IsNullOrEmpty(text))
//         {
//             listBox.Visibility = Visibility.Collapsed;
//             return;
//         }
//
//         foreach (CheckBox cb in listBox.Items.OfType<CheckBox>())
//         {
//             _checkedStates[cb.Content.ToString()] = cb.IsChecked ?? false;
//         }
//
//         List<T> filteredSuggestionList = suggestions
//             .Where(item => item.Name.Contains(text, StringComparison.CurrentCultureIgnoreCase))
//             .ToList();
//
//         List<CheckBox> checkBoxList = filteredSuggestionList
//             .Select(item => new CheckBox { Content = item.Name, Tag = item.Id, IsChecked = _checkedStates.GetValueOrDefault(item.Name, false) })
//             .ToList();
//
//         listBox.ItemsSource = checkBoxList;
//         listBox.Visibility = checkBoxList.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
//     }
//
//     /// <summary>
//     ///     Handles the KeyDown event of the TextBox.
//     /// </summary>
//     /// <param name="sender">The source of the event</param>
//     /// <param name="keyEventArgs">KeyEventArgs event data</param>
//     /// <param name="dataType">The type of data</param>
//     private void TextBox_KeyDown(object sender, KeyEventArgs keyEventArgs, Type dataType)
//     {
//         if (keyEventArgs.Key != Key.Enter)
//         {
//             return;
//         }
//
//         TextBox textBox = sender as TextBox;
//         if (textBox == null)
//         {
//             return;
//         }
//
//         string text = textBox.Text.Trim();
//
//         if (string.IsNullOrEmpty(text))
//         {
//             return;
//         }
//
//         CreateMetadata(dataType, text);
//
//         textBox.Clear();
//     }
//
//     /// <summary>
//     ///     Create Name metadata for the specified type
//     /// </summary>
//     /// <param name="dataType">The type to add</param>
//     /// <param name="text">The text to add to Name for IMetadata</param>
//     private void CreateMetadata(Type dataType, string text)
//     {
//         Dictionary<Type, Func<object>> typeMap = new()
//         {
//             { typeof(Genre), () => new Genre { Id = Guid.NewGuid(), Name = text } },
//             { typeof(Platform), () => new Platform { Id = Guid.NewGuid(), Name = text } },
//             { typeof(Developer), () => new Developer { Id = Guid.NewGuid(), Name = text } },
//             { typeof(Publisher), () => new Publisher { Id = Guid.NewGuid(), Name = text } },
//             { typeof(Series), () => new Series { Id = Guid.NewGuid(), Name = text } },
//             { typeof(Composer), () => new Composer { Id = Guid.NewGuid(), Name = text } },
//             { typeof(Director), () => new Director { Id = Guid.NewGuid(), Name = text } },
//             { typeof(Writer), () => new Writer { Id = Guid.NewGuid(), Name = text } },
//             { typeof(Producer), () => new Producer { Id = Guid.NewGuid(), Name = text } },
//             { typeof(Programmer), () => new Programmer { Id = Guid.NewGuid(), Name = text } },
//             { typeof(Engine), () => new Engine { Id = Guid.NewGuid(), Name = text } },
//             { typeof(AgeRatings), () => new AgeRatings { Id = Guid.NewGuid(), Name = text } },
//             { typeof(Designer), () => new Designer { Id = Guid.NewGuid(), Name = text } },
//             { typeof(Artist), () => new Artist { Id = Guid.NewGuid(), Name = text } }
//         };
//
//         if (!typeMap.TryGetValue(dataType, out Func<object>? createInstance))
//         {
//             return;
//         }
//
//         object instance = createInstance();
//         MethodInfo? method = typeof(MetadataAccessorFactory)
//             .GetMethod("CreateMetadataAccessor")
//             ?.MakeGenericMethod(dataType);
//
//         object? data = method?.Invoke(iMetadataAccessorFactory, null);
//         data?.GetType().GetMethod("AddItemAndSave")?.Invoke(data, [instance]);
//     }
// }
