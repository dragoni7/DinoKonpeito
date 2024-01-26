using Godot;

public partial class UIManager : Node, ISingleInstance<UIManager>
{
    [Export]
    public PackedScene FloatingTextScene;

    private HUD _hud;

    private PauseScreen _pauseScreen;

    public static UIManager GetInstance(Node from)
    {
        return from.GetNode<UIManager>("/root/UiManager");
    }

    public override void _Ready()
    {
        SubscribeEvents();

        _hud = GetNode<HUD>("HUD");
        _pauseScreen = GetNode<PauseScreen>("PauseScreen");
        ShowHUD(false);
    }

    public void SubscribeEvents()
    {
        EventBus eventBus = EventBus.Instance;
        eventBus.Subscribe<ScoreChangeEvent>(OnScoreChanged);
        eventBus.Subscribe<KonpeitoHitEvent>(OnKonpeitoHit);
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed(InputActions.ACTION_ESCAPE) && GameStateManager.GetInstance(this).CurrentState == GameState.Playing)
        {
            GetTree().Paused = true;
            _pauseScreen.Show();
        }
    }

    public void ShowHUD(bool value)
    {
        if (value)
        {
            _hud.Show();
        }
        else
        {
            _hud.Hide();
        }
    }

    public void ShowMessage(string message)
    {
        _hud.MessageLabel.Text = message;
        _hud.MessageLabel.Show();

        _hud.MessageTimer.Start();
    }

    public void SpawnFloatingText(GameConsts.Scores score, Vector2 position)
    {
        FloatingText text = FloatingTextScene.Instantiate<FloatingText>();
        text.SetText(((int)score).ToString());
        text.SetColor(GameConsts.ScoreColors.Get(score));
        text.Position = position;

        if (score >= GameConsts.Scores.High)
        {
            var scene = GD.Load<PackedScene>("res://Scenes/Component/FlashingComponent.tscn");
            FlashingComponent flashComponent = scene.Instantiate<FlashingComponent>();
            flashComponent.FlashColor = GameConsts.ScoreColors.Get(score);
            flashComponent.Target = text;
            text.AddChild(flashComponent);
        }

        AddChild(text);
    }

    private void OnScoreChanged(ScoreChangeEvent e)
    {
        _hud.UpdateScoreLabel(e.NewScore);
    }

    private void OnKonpeitoHit(KonpeitoHitEvent e)
    {
        bool floorCollision = e.GroupsHit.Contains("Floor");

        if (!floorCollision)
        {
            SpawnFloatingText((GameConsts.Scores)e.ScoreOnHit, e.KonpeitoHit.Position);
        }
    }

    async public void ShowGameOver()
    {
        ShowMessage("Game Over");

        await ToSignal(_hud.MessageTimer, Timer.SignalName.Timeout);
    }
}
