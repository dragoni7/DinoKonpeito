using Godot;
using Godot.Collections;

public partial class Konpeito : Node2D
{

    [Export]
	public KonpeitoEffect Effect { get; set; }

    [Export]

    private float _baseSpeed;

    public float BaseSpeed => _baseSpeed;

    private float _currentSpeed;

    public float CurrentSpeed
	{
		get => _currentSpeed;
		set => _currentSpeed = value;
	}

    [Signal]
    public delegate void DestroyedEventHandler(Konpeito konpeito, bool hitFloor);

    public override void _EnterTree()
    {
		_currentSpeed = _baseSpeed;
    }

    private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		QueueFree();
	}

    public void Move()
	{
		Vector2 newPosition = Position;
		newPosition.Y += _currentSpeed;

		Position = newPosition;
	}

	public void ModifySpeed(float modifier)
	{
        _currentSpeed *= modifier;
		GD.Print("Speed modified to " + _currentSpeed);
	}

	public void ResetSpeed()
	{
		_currentSpeed = _baseSpeed;
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
