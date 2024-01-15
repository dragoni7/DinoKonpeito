using Godot;

public partial class FlashingComponent : Node
{
	[Export]
	public Node2D Target;

	[Export]
	public Color FlashColor { get; set; }

	private Timer _timer;

	private Color _currentColor;

    public override void _Ready()
    {
		_timer = GetNode<Timer>("Timer");
		_currentColor = FlashColor;
    }

	public void OnTimerTimeout()
	{
		Target.Modulate = _currentColor;
        NextColor();
    }

	private void NextColor()
	{
        _currentColor = _currentColor == FlashColor ? _currentColor = Colors.White : _currentColor = FlashColor;
	}
}
