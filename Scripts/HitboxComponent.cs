using DinoKonpeito.Component;
using Godot;
using Godot.Collections;

public partial class HitboxComponent : Area2D
{
	[Signal]
	public delegate void HitEventHandler(Array<StringName> groups);

	[Export]
	private HealthComponent _healthComponent;

	[Export]
	private Timer _cdTimer;

	private void OnAreaEntered(Area2D otherArea)
	{
		if (otherArea is HitboxComponent)
		{
            HandleHit(otherArea);
        }
	}

	private void HandleHit(Area2D otherArea)
	{
        EmitSignal(SignalName.Hit, otherArea.GetGroups());
        _healthComponent?.TakeDamage(1f);
        GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
        _cdTimer?.Start();
    }

	private void OnCollisionTimerTimeout()
	{
        GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, false);
    }
}
