using Godot;

public partial class Player : CharacterBody2D
{
    [Signal]
    public delegate void GameOverEventHandler();

    public void OnDeath()
    {
        Hide();
        EmitSignal(SignalName.GameOver);
    }
}
