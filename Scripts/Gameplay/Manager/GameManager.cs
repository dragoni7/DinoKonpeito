using Godot;

public partial class GameManager : Node, ISingleInstance<GameManager>
{
    private int _score;

    private float _spawnTime;

    private double _gameSpeed;

    private Timer _konpeitoTimer;

    private Timer _startTimer;

    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            EventBus.Instance.Raise(new ScoreChangeEvent(_score));
        }
    }

    public static GameManager GetInstance(Node from)
    {
        return from.GetNode<GameManager>("/root/Game/GameManager");
    }

    public override void _Ready()
    {
        _konpeitoTimer = GetNode<Timer>("KonpeitoTimer");
        _startTimer = GetNode<Timer>("StartTimer");

        EventBus eventBus = EventBus.Instance;

        eventBus.Subscribe<DifficultyChangeEvent>(OnDifficultyIncreased);
        eventBus.Subscribe<KonpeitoHitEvent>(OnKonpeitoHit);
        eventBus.Subscribe<GameOverEvent>(OnGameOver, EventPriority.Low);

        InitNewGame();
    }

    public void InitNewGame()
    {
        Score = 0;
        _spawnTime = GameConsts.Konpeito.MaxSpawnTime;
        _startTimer.Start();
        _konpeitoTimer.WaitTime = _spawnTime;
        _konpeitoTimer.Timeout += GetNode<KonpeitoSpawner>("/root/Game/KonpeitoSpawner").OnSpawnKonpeito;
    }

    public async void OnGameOver(GameOverEvent e)
    {
        GameData.Instance.UpdateHighScore(Score);

        await ToSignal(GetTree().CreateTimer(2), "timeout");

        GameStateManager.GetInstance(this).ChangeToState(GameState.ExitingGame);
    }

    public void OnKonpeitoHit(KonpeitoHitEvent e)
    {
        bool floorCollision = e.GroupsHit.Contains("Floor");

        if (!floorCollision)
        {
            DifficultyManager _difficultyManager = DifficultyManager.GetInstance(this);
            Score += e.ScoreOnHit;

            if ((_difficultyManager.Stage + 1) * GameConsts.Difficulty.StageInterval < Score)
            {
                _difficultyManager.NextStage();
            }
        }
    }

    public void OnDifficultyIncreased(DifficultyChangeEvent e)
    {
        _spawnTime -= GameConsts.Konpeito.SpawnTimeReduction;
        _spawnTime = Mathf.Clamp(_spawnTime, 0.75f, GameConsts.Konpeito.MaxSpawnTime);
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
