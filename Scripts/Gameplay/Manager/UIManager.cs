using Godot;

public partial class UIManager : Node, ISingletonNode
{
    [Export]
    public PackedScene FloatingTextScene;

    [Export]
    private HUD _hud;

    public static T GetInstance<T>(Node from) where T : Node
    {
        return from.GetNode<T>("/root/Game/UIManager");
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
        text.Position = position;
        AddChild(text);
    }

    async public void ShowGameOver()
    {
        ShowMessage("Game Over");

        await ToSignal(_hud.MessageTimer, Timer.SignalName.Timeout);
    }
}
