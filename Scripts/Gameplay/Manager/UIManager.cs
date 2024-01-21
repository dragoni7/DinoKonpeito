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

    public void OnScoreChanged(ScoreChangeEvent e)
    {
        _hud.UpdateScoreLabel(e.NewScore);
    }

    public void OnKonpeitoHit(KonpeitoHitEvent e)
    {
        bool floorCollision = e.GroupsHit.Contains("Floor");

        if (!floorCollision)
        {
            FloatingText text = FloatingTextScene.Instantiate<FloatingText>();
            text.SetText(e.ScoreOnHit.ToString());
            text.SetColor(GameConsts.ScoreColors.Get((GameConsts.Scores)e.ScoreOnHit));
            text.Position = e.KonpeitoHit.Position;

            if (e.ScoreOnHit >= (int)GameConsts.Scores.High)
            {
                var scene = GD.Load<PackedScene>("res://Scenes/Component/FlashingComponent.tscn");
                FlashingComponent flashComponent = scene.Instantiate<FlashingComponent>();
                flashComponent.FlashColor = GameConsts.ScoreColors.Get((GameConsts.Scores)e.ScoreOnHit);
                flashComponent.Target = text;
                text.AddChild(flashComponent);
            }

            AddChild(text);
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
