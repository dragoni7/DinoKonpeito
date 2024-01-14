using Godot;
using Godot.Collections;

public partial class Konpeito : Node2D
{

	[Export]
	public KonpeitoEffect Effect;

	[Export]
	public AudioStreamPlayer2D sound;

    [Export]
    public float Speed { get; set; }

	[Export]
	private int _score;

	public int Score => _score;

    [Signal]
    public delegate void DestroyedEventHandler(Konpeito konpeito, bool hitFloor);

    private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		QueueFree();
	}

    public void Move(float modifier)
	{
		Position += Vector2.Down * Speed * modifier;
	}

	public void OnHit(Array<StringName> groups)
	{
		bool hitFloor = false;

		if (groups.Contains("Floor"))
		{
			hitFloor = true;
		}

        EmitSignal(SignalName.Destroyed, this, hitFloor);
    }
}
