using Godot;

public partial class GameManager : Node, ISingleInstance<GameManager>
{
    private int _score;

    private float _spawnTime;

    private double _gameSpeed;

    private DifficultyTracker _difficulty;

    private Timer _konpeitoTimer;

    private Timer _startTimer;

    private Timer _menuTimer;

    private Marker2D _startPosition;

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

    public static GameManager GetInstance(Node from)
    {
        return from.GetNode<GameManager>("/root/Game/GameManager");
    }

    public override void _Ready()
    {
        _konpeitoTimer = GetNode<Timer>("KonpeitoTimer");
        _startTimer = GetNode<Timer>("StartTimer");
        _menuTimer = GetNode<Timer>("MenuReturnTimer");
        _startPosition = GetNode<Marker2D>("StartPosition");
        _konpeitoTimer.Timeout += GetNode<KonpeitoSpawner>("/root/Game/KonpeitoSpawner").OnSpawnKonpeito;
        _difficulty = GetNode<DifficultyTracker>("/root/Game/DifficultyTracker");

        NewGame();
        UIManager.GetInstance(this).ShowMessage("Begin!");
    }

    public void NewGame()
    {
        _spawnTime = GameConsts.Konpeito.MaxSpawnTime;

        // set up player
        Vector2 startPosition = _startPosition.Position;
        PlayerManager playerManager = PlayerManager.GetInstance(this);
        playerManager.SpawnPlayer(startPosition);
        playerManager.Player.GameOver += OnGameOver;

        // set up floor
        FloorManager.GetInstance(this).CreateFloor();

        // start timer
        _startTimer.Start();
        _konpeitoTimer.WaitTime = _spawnTime;
    }

    public void OnIncreaseScore(int amount)
    {
        Score += amount;

        if ((DifficultyTracker.Stage + 1) * GameConsts.Difficulty.StageInterval < Score)
        {
            _difficulty.NextStage();
        }
    }

    public void OnDifficultyIncreased()
    {
        _spawnTime -= GameConsts.Konpeito.SpawnTimeReduction;
        _spawnTime = Mathf.Clamp(_spawnTime, 0.75f, GameConsts.Konpeito.MaxSpawnTime);
    }

    public void OnGameOver()
    {
        _konpeitoTimer.Stop();
        UIManager.GetInstance(this).ShowGameOver();
        _menuTimer.Start();
    }

    private void OnReturnTimerTimeout()
    {
        SceneLoader.GetInstance(this).ChangeToScene("UI/Menu.tscn");
    }

    private void OnStartTimerTimeout()
    {
        _konpeitoTimer.Start();
    }

    private void OnKonpeitoTimerTimeout()
    {
        _konpeitoTimer.WaitTime = Mathf.Clamp(GD.Randfn(_spawnTime, 1f), GameConsts.Konpeito.MinSpawnTime, _spawnTime);
    }
}
