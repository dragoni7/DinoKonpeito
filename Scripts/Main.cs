using Godot;
using System;

public partial class Main : Node2D
{
    [Export]
    public PackedScene KonpeitoScene { get; set; }

    private int _score;
    private float _spawns;

    public override void _Ready()
    {
        NewGame();
    }

    public void GameOver()
    {
        GetNode<Timer>("KonpeitoTimer").Stop();
    }

    public void NewGame()
    {
        _score = 0;

        var player = GetNode<Player>("Player");
        var startPosition = GetNode<Marker2D>("StartPosition");
        player.Start(startPosition.Position);

        GetNode<Timer>("StartTimer").Start();
        GetNode<Timer>("KonpeitoTimer").WaitTime = 5f;
    }

    private void OnStartTmerTimeout()
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
}
