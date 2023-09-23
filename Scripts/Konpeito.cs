using Godot;

public partial class Konpeito : Node2D
{
	[Export]
	public float Speed { get; set; }
	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		QueueFree();
	}

    public override void _Process(double delta)
	{
		Vector2 newPosition = Position;
		newPosition.Y += Speed;

		Position = newPosition;
	}

	public void OnHit()
	{
        QueueFree();
    }
}
