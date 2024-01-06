using Godot;

public partial class HUD : CanvasLayer
{
    [Export]
    public PackedScene FloatingTextScene;

    private const string _scoreText = "Score ";
    public void ShowMessage(string text)
    {
        var message = GetNode<Label>("Message");
        message.Text = text;
        message.Show();

        GetNode<Timer>("MessageTimer").Start();
    }

    public void OnUpdateScore(int score)
    {
        GetNode<Label>("ScoreLabel").Text = _scoreText + score.ToString();
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

        var messageTimer = GetNode<Timer>("MessageTimer");
        await ToSignal(messageTimer, Timer.SignalName.Timeout);
    }

    private void OnMessageTimerTimeout()
    {
        GetNode<Label>("Message").Hide();
    }
}
