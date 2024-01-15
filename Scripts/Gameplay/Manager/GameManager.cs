using Godot;

public partial class GameManager : Node, ISingletonNode
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

    public static T GetInstance<T>(Node from) where T : Node
    {
        return from.GetNode<T>("/root/Game/GameManager");
    }

    public override void _Ready()
    {
        NewGame();
        UIManager.GetInstance<UIManager>(this).ShowMessage("Begin!");
    }

    public void OnIncreaseScore(int amount)
    {
        Score += amount;

        if (Score % 100 == 0)
        {
            GD.Print("Difficulty increase");
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
        UIManager.GetInstance<UIManager>(this).ShowGameOver();
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
