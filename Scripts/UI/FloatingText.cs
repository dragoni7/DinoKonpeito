using Godot;

public partial class FloatingText : Node2D
{
	[Export]
	private Label _label;

	private Tween _tween;

    public override void _Ready()
	{
		_tween = GetTree().CreateTween();
		_tween.Finished += OnTweenFinished;
        _tween.SetEase(Tween.EaseType.Out).SetTrans(Tween.TransitionType.Quart);
        _tween.TweenProperty(this, "scale", Vector2.One * 1.2f, 0.4);
    }

	private void OnTweenFinished()
	{
        _tween.Finished -= OnTweenFinished;
        QueueFree();
	}

	public override void _Process(double delta)
	{
		Position += Vector2.Up;
	}

	public void SetText(string text)
	{
		_label.Text = text;
	}
}
