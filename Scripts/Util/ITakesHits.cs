using Godot.Collections;
using Godot;

public interface ITakesHits
{
    public void OnHit(Array<StringName> groups);
}
