using Godot;

namespace DinoKonpeito.Component
{
    public partial class PlayerMovementComponent : Node2D
    {
        [Export]
        public float Speed { get; set; } = 300f;

        private bool _movingLeft;

        private bool _extending;

        public bool Extending => _extending;

        [Export]
        private CharacterBody2D _characterBody2D;

        public override void _Ready()
        {
            base._Ready();
            _movingLeft = true;
            _extending = false;
        }
        public override void _PhysicsProcess(double delta)
        {
            Vector2 velocity = _characterBody2D.Velocity;

            _extending = Input.IsActionPressed("Up");

            // only move when not extending
            if (!_extending)
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

                _characterBody2D.Velocity = velocity;

                _characterBody2D.MoveAndSlide();
            }
        }

        private void DetermineFlip(float distance)
        {
            if (distance > 0 && _movingLeft)
            {
                _movingLeft = false;
                _characterBody2D.Transform *= Transform2D.FlipX;
            }
            else if (distance < 0 && !_movingLeft)
            {
                _movingLeft = true;
                _characterBody2D.Transform *= Transform2D.FlipX;
            }
        }
    }
}
