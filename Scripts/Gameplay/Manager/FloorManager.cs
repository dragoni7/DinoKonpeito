using Godot;
using System.Collections.Generic;
using System.Linq;

partial class FloorManager : Node, ISingleInstance<FloorManager>
{
    [Export]
    private PackedScene _floorScene;

    private List<Vector2> _destroyedPositions;

    public static FloorManager GetInstance(Node from)
    {
        return from.GetNode<FloorManager>("/root/Game/FloorManager");
    }

    public override void _Ready()
    {
        _destroyedPositions = new(29);
        SubscribeEvents();
        CreateFloor();
    }

    public void SubscribeEvents()
    {
        EventBus.Instance.Subscribe<FloorHitEvent>(OnFloorHit);
    }

    public void CreateFloor()
    {
        for (int x = 64; x < GameConsts.Bounds.GameAreaX; x += 64)
        {
            Floor floor = _floorScene.Instantiate<Floor>();
            floor.Position = new Vector2(x + 32, 1048);
            AddChild(floor);
        }
    }

    public void RestoreFloor()
    {
        if (_destroyedPositions.Count > 0)
        {
            Vector2 playerPos = PlayerManager.GetInstance(this).Player.Position;
            Vector2 closestPos = _destroyedPositions.Aggregate((v1, v2) => v1.DistanceSquaredTo(playerPos) < v2.DistanceSquaredTo(playerPos) ? v1 : v2);
            _destroyedPositions.Remove(closestPos);

            Floor floor = _floorScene.Instantiate<Floor>();
            floor.Position = new Vector2(closestPos.X, 0);

            var hitbox = floor.GetNode<Area2D>("HitboxComponent");
            hitbox.Monitorable = false;
            hitbox.Monitoring = false;
            
            AddChild(floor);

            var tween = GetTree().CreateTween();
            tween.SetEase(Tween.EaseType.Out).SetTrans(Tween.TransitionType.Expo);
            tween.TweenProperty(floor, "position", closestPos, 0.3);
            tween.TweenProperty(hitbox, "monitorable", true, 0.3);
            tween.TweenProperty(hitbox, "monitoring", true, 0.3);
        }
    }

    public void RestoreAllFloor()
    {
        while (_destroyedPositions.Count > 0)
        {
            Vector2 playerPos = PlayerManager.GetInstance(this).Player.Position;
            Vector2 closestPos = _destroyedPositions.Aggregate((v1, v2) => v1.DistanceSquaredTo(playerPos) < v2.DistanceSquaredTo(playerPos) ? v1 : v2);
            _destroyedPositions.Remove(closestPos);

            Floor floor = _floorScene.Instantiate<Floor>();
            floor.Position = new Vector2(closestPos.X, 0);

            var hitbox = floor.GetNode<Area2D>("HitboxComponent");
            hitbox.Monitorable = false;
            hitbox.Monitoring = false;

            AddChild(floor);

            var tween = GetTree().CreateTween();
            tween.SetEase(Tween.EaseType.Out).SetTrans(Tween.TransitionType.Expo);
            tween.TweenProperty(floor, "position", closestPos, 0.3);
            tween.TweenProperty(hitbox, "monitorable", true, 0.3);
            tween.TweenProperty(hitbox, "monitoring", true, 0.3);
        }
    }

    private void OnFloorHit(FloorHitEvent e)
    {
        _destroyedPositions.Add(e.HitFloor.Position);
        e.HitFloor.QueueFree();
    }

    private void SpawnFloor(Vector2 position)
    {
        Floor floor = _floorScene.Instantiate<Floor>();
        floor.Position = position;
        AddChild(floor);
    }
}
