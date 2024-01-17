using Godot;

public partial class HUD : CanvasLayer
{

    [Export]
    private Label ScoreLabel { get; set; }

    private Tween _tween;

    private float _scoreFontSize;

    private const string ScoreText = "Score ";

    private const float TweenDuration = 0.15f;

    [Export]
    public Label MessageLabel { get; private set; }

    [Export]
    public Timer MessageTimer { get; private set; }

    public override void _Ready()
    {
        _scoreFontSize = ScoreLabel.GetThemeFontSize("font_size");
    }

    public void UpdateScoreLabel(int score)
    {
        ScoreLabel.Text = ScoreText + score.ToString();

        _tween = GetTree().CreateTween();
        _tween.Finished += OnTweenFinished;
        _tween.SetEase(Tween.EaseType.Out).SetTrans(Tween.TransitionType.Linear);
        _tween.TweenProperty(ScoreLabel, "theme_override_font_sizes/font_size", _scoreFontSize + 12f, TweenDuration);
    }

    private void OnTweenFinished()
    {
        _tween.Finished -= OnTweenFinished;

        _tween = GetTree().CreateTween();
        _tween.SetEase(Tween.EaseType.In).SetTrans(Tween.TransitionType.Linear);
        _tween.TweenProperty(ScoreLabel, "theme_override_font_sizes/font_size", _scoreFontSize, TweenDuration);
    }

    private void OnMessageTimerTimeout()
    {
        MessageLabel.Hide();
    }
}
