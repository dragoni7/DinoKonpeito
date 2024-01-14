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

        if (Score % 100 == 0)
        {
            _spawnTime -= 0.25;
            GetNode<Timer>("KonpeitoTimer").WaitTime = Mathf.Clamp(_spawnTime, 0.75, 5);
        }
    }

    public void NewGame()
    {
        Score = 0;
        _spawnTime = 3.5;

        // set up player
        Vector2 startPosition = GetNode<Marker2D>("StartPosition").Position;
        PlayerManager playerManager = PlayerManager.GetInstance<PlayerManager>(this);
        playerManager.SpawnPlayer(startPosition);
        playerManager.Player.GameOver += OnGameOver;

        // set up floor
        FloorManager.GetInstance<FloorManager>(this).CreateFloor();

        // start timer
        GetNode<Timer>("StartTimer").Start();
        GetNode<Timer>("KonpeitoTimer").WaitTime = _spawnTime;
    }

    public void OnGameOver()
    {
        KonpeitoManager.GetInstance<KonpeitoManager>(this).ClearKonpeito();
        GetNode<Timer>("KonpeitoTimer").Stop();
        GetNode<HUD>("HUD").ShowGameOver();
        GetNode<Timer>("MenuReturnTimer").Start();
    }

    private void OnReturnTimerTimeout()
    {
        SceneLoader.GetInstance<SceneLoader>(this).ChangeToScene("UI/Menu.tscn");
    }

    private void OnStartTimerTimeout()
    {
        GetNode<Timer>("KonpeitoTimer").Start();
    }

    private void OnKonpeitoTimerTimeout()
    {
        KonpeitoManager.GetInstance<KonpeitoManager>(this).SpawnKonpeito();
    }
}
