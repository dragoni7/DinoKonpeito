using Godot;

public partial class HUD : CanvasLayer
{
    [Export]
    public Label MessageLabel { get; private set; }

    [Export]
    public Timer MessageTimer { get; private set; }

    [Export]
    private Label ScoreLabel { get; set; }

    private const string _scoreText = "Score ";

    public void UpdateScoreLabel(int score)
    {
        ScoreLabel.Text = _scoreText + score.ToString();
    }

    private void OnMessageTimerTimeout()
    {
        MessageLabel.Hide();
    }
}
