using Godot;
using Godot.Collections;

public class GameData
{
    private static GameData _instance = null;
    private static readonly object _lock = new();

    private const string _dataPath = "user://konpeito.dat";

    public int HighScore { get; set; } = 0;

    public static GameData Instance
    {
        get
        {
            lock (_lock)
            {
                _instance ??= new GameData();

                return _instance;
            }
        }
    }

    private GameData()
    {
        if (!ReadData()) {
            SaveData();
        }
    }

    public void SaveData()
    {
        using var dataFile = FileAccess.Open(_dataPath, FileAccess.ModeFlags.Write);

        Dictionary<string, Variant> gameData = new()
        {
            { "HighScore", HighScore }
        };

        var jsonString = Json.Stringify(gameData);

        dataFile.StoreLine(jsonString);
    }

    public bool ReadData()
    {
        if (!FileAccess.FileExists(_dataPath))
        {
            return false;
        }


        using var dataFile = FileAccess.Open(_dataPath, FileAccess.ModeFlags.Read);

        while (dataFile.GetPosition() < dataFile.GetLength())
        {
            var jsonString = dataFile.GetLine();

            var json = new Json();
            var parseResult = json.Parse(jsonString);

            if (parseResult != Error.Ok)
            {
                GD.Print($"JSON Parse Error: {json.GetErrorMessage()} in {jsonString} at line {json.GetErrorLine()}");
                continue;
            }

            var data = new Dictionary<string, Variant>((Dictionary)json.Data);
            HighScore = (int)data["HighScore"];
        }

        return true;
    }
}
