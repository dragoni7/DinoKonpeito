using Godot;

public partial class Player : CharacterBody2D
{
    [Signal]
    public delegate void GameOverEventHandler();
    public override void _Ready()
    {
        Hide();
    }

    public void Start(Vector2 position)
    {
        Position = position;
        Show();
    }

    public void OnDeath()
    {
        Hide();
        EmitSignal(SignalName.GameOver);
    }
}
