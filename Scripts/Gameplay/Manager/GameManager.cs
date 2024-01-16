using Godot;

public partial class GameManager : Node, ISingletonNode
{
    private int _score;

    private float _spawnTime;

    private double _gameSpeed;

    private DifficultyTracker _difficulty;

    private Timer _konpeitoTimer;

    private Timer _startTimer;

    private Timer _menuTimer;

    private Marker2D _startPosition;

    private const float _minSpawnTime = 0.1f;

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

    public static T GetInstance<T>(Node from) where T : Node
    {
        return from.GetNode<T>("/root/Game/GameManager");
    }

    public override void _Ready()
    {
        _konpeitoTimer = GetNode<Timer>("KonpeitoTimer");
        _startTimer = GetNode<Timer>("StartTimer");
        _menuTimer = GetNode<Timer>("MenuReturnTimer");
        _startPosition = GetNode<Marker2D>("StartPosition");
        _konpeitoTimer.Timeout += KonpeitoManager.GetInstance<KonpeitoManager>(this).OnSpawnKonpeito;

        _difficulty = GetNode<DifficultyTracker>("/root/Game/DifficultyTracker");
        NewGame();
        UIManager.GetInstance<UIManager>(this).ShowMessage("Begin!");
    }

    public void NewGame()
    {
        Score = 0;
        _spawnTime = 3;

        // set up player
        Vector2 startPosition = _startPosition.Position;
        PlayerManager playerManager = PlayerManager.GetInstance<PlayerManager>(this);
        playerManager.SpawnPlayer(startPosition);
        playerManager.Player.GameOver += OnGameOver;

        // set up floor
        FloorManager.GetInstance<FloorManager>(this).CreateFloor();

        // start timer
        _startTimer.Start();
        _konpeitoTimer.WaitTime = _spawnTime;
    }

    public void OnIncreaseScore(int amount)
    {
        Score += amount;

        if ((DifficultyTracker.Stage + 1) * 5000 < Score)
        {
            _difficulty.NextStage();
        }
    }

    public void OnDifficultyIncreased(int stage)
    {
        GD.Print("Difficulty increase");
        _spawnTime -= 0.5f;
        _spawnTime = Mathf.Clamp(_spawnTime, 1f, 3f);
    }

    public void OnGameOver()
    {
        KonpeitoManager.GetInstance<KonpeitoManager>(this).ClearKonpeito();
        _konpeitoTimer.Stop();
        UIManager.GetInstance<UIManager>(this).ShowGameOver();
        _menuTimer.Start();
    }

    private void OnReturnTimerTimeout()
    {
        SceneLoader.GetInstance<SceneLoader>(this).ChangeToScene("UI/Menu.tscn");
    }

    private void OnStartTimerTimeout()
    {
        _konpeitoTimer.Start();
    }

    private void OnKonpeitoTimerTimeout()
    {
        _konpeitoTimer.WaitTime = Mathf.Clamp(GD.Randfn(_spawnTime, 1f), _minSpawnTime, _spawnTime);
    }
}
