using static GameManager.Core.Data.Overloads;
using static GameManager.Core.Data.ReusableInstances.Developers;
using static GameManager.Core.Data.ReusableInstances.Publishers;
using static GameManager.Core.Data.ReusableInstances.Series;
using static GameManager.Core.Data.ReusableInstances.Genres;
using static GameManager.Core.Data.ReusableInstances.Platforms;

namespace GameManager.Core.Data;

public static class GameImports{public static List<Game> GetGames(){return[

Game("Dark Souls", developers: [FromSoftware], publishers: [Bandai_Namco], dateWw: D(2011, 09, 22), series: [Souls], genres: [Action_RPG], platforms: [PS3, Windows, Xbox360]),
Game("Dark Souls: Remastered", developers: [QLOC], publishers: [Bandai_Namco], dateWw: D(2018, 05, 24), series: [Souls], genres: [Action_RPG], platforms: [PS4, Windows, Xbox1, Switch]),
Game("Dark Souls 2", developers: [FromSoftware], publishers: [Bandai_Namco], dateWw: D(2014, 03, 11), series: [Souls], genres: [Action_RPG], platforms: [PS3, Windows, Xbox360]),
Game("Dark Souls 2: Scholar of the First Sin", developers: [FromSoftware], publishers: [Bandai_Namco], dateWw: D(2015, 02, 5), series: [Souls], genres: [Action_RPG], platforms: [PS4, Windows, Xbox1]),
Game("Dark Souls 3", developers: [FromSoftware], publishers: [Bandai_Namco], dateWw: D(2016, 03, 24), series: [Souls], genres: [Action_RPG], platforms: [PS4, Windows, Xbox1]),
Game("Elden Ring", developers: [FromSoftware], publishers: [Bandai_Namco], dateWw: D(2022, 02, 25), series: [Souls], genres: [Action_RPG], platforms: [PS4, PS5, Windows, Xbox1, XboxSX]),

];}}