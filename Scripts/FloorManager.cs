using Godot;
using System.Collections.Generic;
partial class FloorManager : Node
{
    [Export]
    public PackedScene FloorScene { get; set; }

    private List<Vector2> _destroyedPositions;

    public override void _Ready()
    {
        _destroyedPositions = new();
    }

    public void CreateFloor()
    {
        for (int x = 0; x < 1920; x += 64)
        {
            Floor floor = FloorScene.Instantiate<Floor>();
            floor.Position = new Vector2(x + 32, 1048);
            floor.Destroyed += OnDestroyFloor;
            AddChild(floor);
        }
    }

    private void OnDestroyFloor(Vector2 position)
    {
        _destroyedPositions.Add(position);
    }

    public void RestoreRandomFloor()
    {
        if (_destroyedPositions.Count > 0)
        {
            Floor floor = FloorScene.Instantiate<Floor>();
            floor.Position = _destroyedPositions[GD.RandRange(0, _destroyedPositions.Count)];
            floor.Destroyed += OnDestroyFloor;
            AddChild(floor);
        }
    }
}
