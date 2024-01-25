using Godot;

public partial class UIManager : Node, ISingleInstance<UIManager>
{
    [Export]
    public PackedScene FloatingTextScene;

    [Export]
    private HUD _hud;

    public static UIManager GetInstance(Node from)
    {
        return from.GetNode<UIManager>("/root/Game/UIManager");
    }

    public override void _Ready()
    {
        EventBus eventBus = EventBus.Instance;
        eventBus.Subscribe<ScoreChangeEvent>(OnScoreChanged);
        eventBus.Subscribe<KonpeitoHitEvent>(OnKonpeitoHit);
        eventBus.Subscribe<GameOverEvent>(OnGameOver);
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

    private void OnGameOver(GameOverEvent e)
    {
        ShowGameOver();
    }

    async public void ShowGameOver()
    {
        ShowMessage("Game Over");

        await ToSignal(_hud.MessageTimer, Timer.SignalName.Timeout);
    }
}
