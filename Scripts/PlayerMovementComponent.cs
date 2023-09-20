using Godot;

namespace DinoKonpeito.Component
{
    public partial class PlayerMovementComponent : Node2D
    {

        private bool _movingLeft;

        private Vector2 _screenSize;

        [Export]
        private CharacterBody2D _characterBody;

        [Export]
        public float Speed { get; set; } = 300f;
        public bool CanMove { get; set; }

        [Export]
        public AnimatedSprite2D _animatedSprite2D;

        public override void _Ready()
        {
            base._Ready();
            _movingLeft = true;
            _screenSize = GetViewportRect().Size;
            CanMove = true;
        }
        public override void _Process(double delta)
        {
            Vector2 velocity = Vector2.Zero;

            // only move when not extending
            if (CanMove)
            {
                // Get the input direction and handle the movement/deceleration.
                float distance = Input.GetAxis("Left", "Right");

                DetermineFlip(distance);

                if (distance != 0f)
                {
                    velocity.X = distance * Speed;
                }
                else
                {
                    velocity.X = Mathf.MoveToward(distance, 0, Speed);
                }

                _characterBody.Position += velocity * (float)delta;
                _characterBody.Position = new Vector2(
                    x: Mathf.Clamp(_characterBody.Position.X, 0, _screenSize.X),
                    y: Mathf.Clamp(_characterBody.Position.Y, 0, _screenSize.Y));
            }

            if (velocity.Length() > 0)
            {
                _animatedSprite2D.Play();
            }
            else
            {
                _animatedSprite2D.Stop();
            }
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
