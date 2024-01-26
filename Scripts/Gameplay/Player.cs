using Godot;
using Godot.Collections;

public partial class Player : CharacterBody2D, ITakesHits
{
    public void OnHit(Array<StringName> groups)
    {
        GameStateManager.GetInstance(this).ChangeToState(GameState.GameOver);
    }
}
