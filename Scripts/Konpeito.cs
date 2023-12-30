using Godot;

public partial class Konpeito : Node2D
{
    [Export]
	public float Speed { get; set; }

    [Signal]
    public delegate void DestroyedEventHandler(Konpeito konpeito);

    private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		QueueFree();
	}

    public void Move()
	{
		Vector2 newPosition = Position;
		newPosition.Y += Speed;

		Position = newPosition;
	}

	public void OnHit()
	{
        EmitSignal(SignalName.Destroyed, this);
    }
}
