using Godot;

public partial class HUD : CanvasLayer
{
    private Label _scoreLabel;

    private Label _highScoreLabel;

    private Tween _tween;

    private float _scoreFontSize;

    private const string ScoreText = "Score ";

    private const float TweenDuration = 0.15f;

    public Label MessageLabel { get; private set; }

    public Timer MessageTimer { get; private set; }

    public override void _Ready()
    {
        _scoreLabel = GetNode<Label>("ScoreLabel");
        _highScoreLabel = GetNode<Label>("HighScoreLabel");
        _scoreFontSize = _scoreLabel.GetThemeFontSize("font_size");
        MessageLabel = GetNode<Label>("MessageLabel");
        MessageTimer = GetNode<Timer>("MessageTimer");
    }

    public void UpdateScoreLabel(int score)
    {
        _scoreLabel.Text = ScoreText + score.ToString();

        if (score > 0)
        {
            _tween = GetTree().CreateTween();
            _tween.Finished += OnTweenFinished;
            _tween.SetEase(Tween.EaseType.Out).SetTrans(Tween.TransitionType.Linear);
            _tween.TweenProperty(_scoreLabel, "theme_override_font_sizes/font_size", _scoreFontSize + 12f, TweenDuration);
        }
    }

    public void UpdateHighScore(int score)
    {
        _highScoreLabel.Text = "HighScore: " + score;
    }

    private void OnTweenFinished()
    {
        _tween = GetTree().CreateTween();
        _tween.SetEase(Tween.EaseType.In).SetTrans(Tween.TransitionType.Linear);
        _tween.TweenProperty(_scoreLabel, "theme_override_font_sizes/font_size", _scoreFontSize, TweenDuration);
    }

    private void OnMessageTimerTimeout()
    {
        MessageLabel.Hide();
    }
}
