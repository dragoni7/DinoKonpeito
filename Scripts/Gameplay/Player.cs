using Godot;
using Godot.Collections;

public partial class Player : CharacterBody2D, ITakesHits
{
    [Signal]
    public delegate void GameOverEventHandler();

    public void OnHit(Array<StringName> groups)
    {
        Hide();
        EmitSignal(SignalName.GameOver);
    }
}
