using Godot;

namespace DinoKonpeito.Component
{
    public partial class PlayerMovementComponent : Node2D
    {
        [Export]
        public float Speed { get; set; } = 300f;

        [Export]
        private CharacterBody2D _characterBody2D;
        public override void _PhysicsProcess(double delta)
        {
            Vector2 velocity = _characterBody2D.Velocity;

            // Handle Extend.
            if (Input.IsActionPressed("Up"))
                GD.Print("Extended");

            // Get the input direction and handle the movement/deceleration.
            float distance = Input.GetAxis("Left", "Right");

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
}
