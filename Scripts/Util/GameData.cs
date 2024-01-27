using Godot;
using Godot.Collections;

public class GameData
{
    private static GameData _instance = null;
    private static readonly object _lock = new();

    private const string _dataPath = "user://konpeito.dat";

    public int HighScore { get; set; }

    public double MusicVolume { get; set; }

    public double SFXVolume { get; set; }

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

    }

    public void UpdateHighScore(int score)
    {
        if (score > HighScore)
        {
            HighScore = score;
            EventBus.Instance.Raise(new GameDataUpdatedEvent(HighScore));
        }
    }

    public void Reset()
    {
        HighScore = 0;
        EventBus.Instance.Raise(new GameDataUpdatedEvent(HighScore));
        SaveData();
    }

    public void SaveData()
    {
        using var dataFile = FileAccess.Open(_dataPath, FileAccess.ModeFlags.Write);

        Dictionary<string, Variant> gameData = new()
        {
            { "HighScore", HighScore },
            { "MusicVolume", MusicVolume },
            { "SFXVolume", SFXVolume }
        };

        var jsonString = Json.Stringify(gameData);

        dataFile.StoreLine(jsonString);
    }

    public bool ReadData()
    {
        if (!FileAccess.FileExists(_dataPath))
        {
            HighScore = 0;
            MusicVolume = 50;
            SFXVolume = 50;
            SaveData();
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
            MusicVolume = (double)data["MusicVolume"];
            SFXVolume = (double)data["SFXVolume"];
            UpdateHighScore((int)data["HighScore"]);
        }

        return true;
    }
}
