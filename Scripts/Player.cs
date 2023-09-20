using Godot;

public partial class Player : CharacterBody2D
{

    public override void _Ready()
    {
        base._Ready();
        Hide();
    }

    public void Start(Vector2 position)
    {
        Position = position;
        Show();
    }
}
