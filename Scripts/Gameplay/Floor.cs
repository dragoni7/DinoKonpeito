using Godot;
using Godot.Collections;

public partial class Floor : Node2D, ITakesHits
{
    public void OnHit(Array<StringName> groups)
    {
        EventBus.Instance.Raise(new FloorHitEvent(this));
    }
}
