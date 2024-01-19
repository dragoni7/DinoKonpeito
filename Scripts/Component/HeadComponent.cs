using DinoKonpeito.Component;
using Godot;
using Godot.Collections;

public partial class HeadComponent : Node2D, ITakesHits
{
	[Export]
	private PlayerMovementComponent _playerMovement;

	[Export]
	private Vector2 _step = new Vector2(2f, -8f);

    private HitboxComponent _hitbox;

    private Tween _tween;

    private Vector2 origin;

    private bool _notTweening;

    public override void _Ready()
    {
        origin = Position;
        _hitbox = GetNode<HitboxComponent>("HitboxComponent");
    }

    public override void _PhysicsProcess(double delta)
	{
        _notTweening = _tween == null || !_tween.IsRunning();

        if (Input.IsActionPressed("Up") && GetViewportRect().HasPoint(GlobalPosition) && _notTweening)
		{
            // extend head position
            _hitbox.Monitorable = true;
            Position += _step;
            _playerMovement.CanMove = false;
        }
		else if (Position.Y < 0)
        {
            Return();
        }
    }

    public void OnHit(Array<StringName> groups)
    {
        Return();
    }

    private void OnHeadReturned()
    {
        _playerMovement.CanMove = true;
        _tween.Finished -= OnHeadReturned;
    }

    private void Return()
    {
        if (_notTweening)
        {
            // use tweening to bring head back to origin
            _tween = GetTree().CreateTween();
            _tween.Finished += OnHeadReturned;
            _tween.SetEase(Tween.EaseType.In).SetTrans(Tween.TransitionType.Expo);
            _tween.TweenProperty(this, "position", origin, 0.15f);
            _tween.TweenProperty(_hitbox, "monitorable", false, 0.15f);
        }
    }
}
