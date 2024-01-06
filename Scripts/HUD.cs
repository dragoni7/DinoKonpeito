using Godot;

public partial class HUD : CanvasLayer
{
    private const string _scoreText = "Score ";
    public void ShowMessage(string text)
    {
        var message = GetNode<Label>("Message");
        message.Text = text;
        message.Show();

        GetNode<Timer>("MessageTimer").Start();
    }

    public void UpdateScore(int score)
    {
        GetNode<Label>("ScoreLabel").Text = _scoreText + score.ToString();
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
