using Godot;
using System.Collections.Generic;

public partial class Main : Node2D
{
    [Export]
    public PackedScene KonpeitoScene { get; set; }

    [Export]
    public PackedScene FloorScene { get; set; }

    private int _score;
    private float _spawns;
    private List<Vector2> _destroyedPositions;

    public override void _Ready()
    {
        NewGame();
        var hud = GetNode<HUD>("HUD");
        hud.UpdateScore(_score);
        hud.ShowMessage("Begin!");
        _destroyedPositions = new();
    }

    public override void _Process(double delta)
    {
        
    }

    public void NewGame()
    {
        _score = 0;

        // set up player
        var player = GetNode<Player>("Player");
        var startPosition = GetNode<Marker2D>("StartPosition");
        player.Start(startPosition.Position);

        // set up floor
        CreateGround();

        GetNode<Timer>("StartTimer").Start();
        GetNode<Timer>("KonpeitoTimer").WaitTime = 3f;
    }

    public void OnGameOver()
    {
        GetTree().CallGroup("Konpeito", Node.MethodName.QueueFree);
        GetNode<Timer>("KonpeitoTimer").Stop();
        GetNode<HUD>("HUD").ShowGameOver();
        GetNode<Timer>("MenuReturnTimer").Start();
    }

    private void OnReturnTimerTimeout()
    {
        GetTree().ChangeSceneToFile("res://Scenes/UI/Menu.tscn");
    }

    private void OnStartTimerTimeout()
    {
        GetNode<Timer>("KonpeitoTimer").Start();
    }

    private void OnKonpeitoTimerTimeout()
    {
        Konpeito konpeito = KonpeitoScene.Instantiate<Konpeito>();

        var spawnLocation = GetNode<PathFollow2D>("Path2D/SpawnPoint");
        spawnLocation.ProgressRatio = GD.Randf();

        konpeito.Position = spawnLocation.Position;
        konpeito.Speed = GD.RandRange(1, 2);

        AddChild(konpeito);
        _spawns++;
    }

    private void CreateGround()
    {

        for (int x = 0; x < 1920; x+= 64)
        {
            Floor floor = FloorScene.Instantiate<Floor>();
            floor.Position = new Vector2(x + 32, 1048);
            floor.Destroyed += OnFloorDestroyed;
            AddChild(floor);
        }
    }

    private void OnFloorDestroyed(Vector2 position)
    {
        _destroyedPositions.Add(position);
    }

    private void OnRestoreRandomFloor()
    {
        Floor floor = FloorScene.Instantiate<Floor>();
        floor.Position = _destroyedPositions[GD.RandRange(0, _destroyedPositions.Count)];
        AddChild(floor);
    }
}
