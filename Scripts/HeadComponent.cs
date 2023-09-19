using DinoKonpeito.Component;
using Godot;

public partial class HeadComponent : Node2D
{
	[Export]
	private PlayerMovementComponent _playerMovement;

	[Export]
	private Vector2 _step = new Vector2(2f, -8f);

    private Area2D _head;

    private Tween _tween;
    public override void _Ready()
	{
		_head = GetNode<Area2D>("Area2D");
    }

	public override void _Process(double delta)
	{
		if (!_playerMovement.Extending)
		{
			// use tweening to bring head back to origin
			if (_head.Position.Y < 0 && (_tween == null || !_tween.IsRunning()))
			{
                _tween = GetTree().CreateTween().SetEase(Tween.EaseType.In).SetTrans(Tween.TransitionType.Back);
                _tween.TweenProperty(_head, "position", Vector2.Zero, 0.5f);
            }
        }
		else
		{
			// extend head position
            _head.Position += _step;
        }
    }
}
