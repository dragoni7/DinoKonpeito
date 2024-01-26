using Godot;
using Godot.Collections;

public partial class Player : CharacterBody2D, ITakesHits
{
    public void OnHit(Array<StringName> groups)
    {
        Hide();
        EventBus.Instance.Raise(new GameOverEvent());
    }
}
