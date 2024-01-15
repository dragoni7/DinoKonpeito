using DinoKonpeito.Component;
using Godot;

public partial class HeadComponent : Node2D
{
	[Export]
	private PlayerMovementComponent _playerMovement;

    [Export]
    private HitboxComponent _hitbox;

	[Export]
	private Vector2 _step = new Vector2(2f, -8f);

    private Tween _tween;

    private Vector2 origin;

    public override void _Ready()
    {
        origin = Position;
        _hitbox.Monitorable = false;
    }

    public override void _Process(double delta)
	{
		if (Input.IsActionPressed("Up"))
		{
            // extend head position
            Position += _step;
            _playerMovement.CanMove = false;
            _hitbox.Monitorable = true;
        }
		else if (Position.Y < 0 && (_tween == null || !_tween.IsRunning()))
        {
            // use tweening to bring head back to origin
            _tween = GetTree().CreateTween();
            _tween.Finished += OnHeadReturned;
            _tween.SetEase(Tween.EaseType.In).SetTrans(Tween.TransitionType.Back);
            _tween.TweenProperty(this, "position", origin, 0.3f);
            _hitbox.Monitorable = false;
        }
    }

    private void OnHeadReturned()
    {
        _playerMovement.CanMove = true;
        _tween.Finished -= OnHeadReturned;
    }
}
