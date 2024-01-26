using Godot;
using Godot.Collections;

public partial class Game : Node2D
{
    [Export]
    private Array<PackedScene> _systems;

    public override void _EnterTree()
    {
        foreach (PackedScene system in _systems)
        {
            var node = system.Instantiate();
            AddChild(node);
        }
    }
}
