using Godot;
using Godot.Collections;

public partial class Konpeito : Node2D, ITakesHits
{

	[Export]
	public KonpeitoEffect Effect;

    [Export]
    public float Speed { get; set; }

    [Signal]
    public delegate void DestroyedEventHandler(Konpeito konpeito, Array<StringName> groups);

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
        EmitSignal(SignalName.Destroyed, this, groups);
    }
}
