using Godot;

namespace DinoKonpeito.Component
{
    public partial class PlayerMovementComponent : Node2D
    {
        private bool _movingLeft;

        [Export]
        private CharacterBody2D _characterBody;

        private RayCast2D _rayCast;

        [Export]
        public float Speed { get; set; } = GameConsts.Player.BaseSpeed;
        public bool CanMove { get; set; }

        [Export]
        public AnimatedSprite2D AnimatedSprite2D;

        public override void _Ready()
        {
            base._Ready();
            _rayCast = GetNode<RayCast2D>("RayCast2D");
            _movingLeft = true;
            CanMove = true;
        }
        public override void _Process(double delta)
        {
            Vector2 velocity = Vector2.Zero;

            // only move when not extending
            if (CanMove)
            {
                // Get the input direction and handle the movement/deceleration.
                float distance = Input.GetAxis(InputActions.ACTION_MOVE_LEFT, InputActions.ACTION_MOVE_RIGHT);

                DetermineFlip(distance);

                if (distance != 0f)
                {
                    velocity.X = distance * Speed;
                }
                else
                {
                    velocity.X = Mathf.MoveToward(distance, 0, Speed);
                }

                Vector2 newPosition = _characterBody.Position;
                Vector2 movement = velocity * (float)delta;
                newPosition += movement;
                newPosition = new Vector2(
                    x: Mathf.Clamp(newPosition.X, 64, GameConsts.Bounds.GameAreaX),
                    y: newPosition.Y);

                if (HasGround(movement) && _characterBody.Position != newPosition)
                {
                    EventBus.Instance.Raise(new PlayerPositionChangedEvent(newPosition, _characterBody.Position));
                    _characterBody.Position = newPosition;
                }
            }

            if (velocity.Length() > 0)
            {
                AnimatedSprite2D.Play();
            }
            else
            {
                AnimatedSprite2D.Stop();
            }
        }

        private bool HasGround(Vector2 movement)
        {
            bool value;
            _rayCast.Position += movement;
            value = _rayCast.IsColliding();
            _rayCast.Position -= movement;
            return value;
        }

        private void DetermineFlip(float distance)
        {
            if (distance > 0 && _movingLeft)
            {
                _movingLeft = false;
                _characterBody.Transform *= Transform2D.FlipX;
            }
            else if (distance < 0 && !_movingLeft)
            {
                _movingLeft = true;
                _characterBody.Transform *= Transform2D.FlipX;
            }
        }
    }
}
