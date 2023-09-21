using DinoKonpeito.Component;
using Godot;

public partial class HitboxComponent : Area2D
{
	[Signal]
	public delegate void HitEventHandler();

	[Export]
	private HealthComponent _healthComponent;

	[Export]
	private Timer _cdTimer;

	private void OnBodyEntered(PhysicsBody2D body)
	{
		if (body.IsInGroup("Konpeito"))
		{
            EmitSignal(SignalName.Hit);
            _healthComponent?.TakeDamage(1f);
            GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
            _cdTimer?.Start();
        }
	}

	private void OnCollisionTimerTimeout()
	{
        GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, false);
    }
}
