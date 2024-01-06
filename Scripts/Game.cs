using Godot;

public partial class Game : Node2D
{
    private int _score;

    private double _spawnTime;

    private double _gameSpeed;

    private int Score
    {
        get => _score;
        set
        {
            _score = value;
            EmitSignal(SignalName.ScoreChanged, _score);
        }
    }

    [Signal]
    public delegate void ScoreChangedEventHandler(int score);

    public override void _Ready()
    {
        NewGame();
        var hud = GetNode<HUD>("HUD");
        hud.ShowMessage("Begin!"); 
    }

    public void OnIncreaseScore(int amount)
    {
        Score += amount;

        if (Score % 15 == 0)
        {
            _spawnTime -= 0.25;
            Mathf.Clamp(_spawnTime, 0.25, 5);
            GetNode<Timer>("KonpeitoTimer").WaitTime = _spawnTime;
        }
    }

    public void NewGame()
    {
        Score = 0;
        _spawnTime = 3.5;

        // set up player
        var player = GetNode<Player>("Player");
        var startPosition = GetNode<Marker2D>("StartPosition");
        player.Start(startPosition.Position);

        // set up floor
        GetNode<FloorManager>("FloorManager").CreateFloor();

        // start timer
        GetNode<Timer>("StartTimer").Start();
        GetNode<Timer>("KonpeitoTimer").WaitTime = _spawnTime;
    }

    public void OnGameOver()
    {
        GetNode<KonpeitoManager>("KonpeitoManager").ClearKonpeito();
        GetNode<Timer>("KonpeitoTimer").Stop();
        GetNode<HUD>("HUD").ShowGameOver();
        GetNode<Timer>("MenuReturnTimer").Start();
    }

    private void OnReturnTimerTimeout()
    {
        GetNode<SceneLoader>("/root/SceneLoader").ChangeToScene("UI/Menu.tscn");
    }

    private void OnStartTimerTimeout()
    {
        GetNode<Timer>("KonpeitoTimer").Start();
    }

    private void OnKonpeitoTimerTimeout()
    {
        GetNode<KonpeitoManager>("KonpeitoManager").SpawnKonpeito();
    }
}
