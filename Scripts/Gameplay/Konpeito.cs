using Godot;
using Godot.Collections;

[Tool]
public partial class Konpeito : Node2D, ITakesHits
{

	[Export]
	public KonpeitoEffect Effect;

    [Export]
    public float Speed { get; set; }

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
		EventBus.Instance.Raise(new KonpeitoHitEvent(this, groups));
    }
}
