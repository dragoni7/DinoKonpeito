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

    public void ShowMessage(string message)
    {
        _hud.MessageLabel.Text = message;
        _hud.MessageLabel.Show();

        _hud.MessageTimer.Start();
    }

    public void OnUpdateScore(int score)
    {
        _hud.UpdateScoreLabel(score);
    }

    public void OnDisplayScoreText(int amount, Vector2 position)
    {
        FloatingText text = FloatingTextScene.Instantiate<FloatingText>();
        text.SetText(amount.ToString());
        text.SetColor(GameConsts.ScoreColors.Get((GameConsts.Scores)amount));
        text.Position = position;

        if (amount >= (int)GameConsts.Scores.High)
        {
            GD.Print("flashing text");
            var scene = GD.Load<PackedScene>("res://Scenes/Component/FlashingComponent.tscn");
            FlashingComponent flashComponent = scene.Instantiate<FlashingComponent>();
            flashComponent.FlashColor = GameConsts.ScoreColors.Get((GameConsts.Scores)amount);
            flashComponent.Target = text;
            text.AddChild(flashComponent);
        }

        AddChild(text);
    }

    async public void ShowGameOver()
    {
        ShowMessage("Game Over");

        await ToSignal(_hud.MessageTimer, Timer.SignalName.Timeout);
    }
}
