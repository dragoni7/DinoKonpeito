using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

partial class KonpeitoManager : Node
{
    [Export]
    public PackedScene KonpeitoScene { get; set; }

    private float _spawns;

    public float Spawns => _spawns;

    public override void _Process(double delta)
    {
        GetTree().CallGroup("Konpeito", Konpeito.MethodName.Move);
    }

    public void SpawnKonpeito()
    {
        Konpeito konpeito = KonpeitoScene.Instantiate<Konpeito>();
        konpeito.Destroyed += OnKonpeitoDestroyed;

        var spawnLocation = GetNode<PathFollow2D>("Path2D/SpawnPoint");
        spawnLocation.ProgressRatio = GD.Randf();

        konpeito.Position = spawnLocation.Position;
        konpeito.Speed = GD.RandRange(1, 2) + (_spawns * 0.05f);

        AddChild(konpeito);
        _spawns++;
    }

    public void OnKonpeitoDestroyed(Konpeito konpeito)
    {
        konpeito.QueueFree();
        GetParent<Game>().IncreaseScore();
    }

    public void ClearKonpeito()
    {
        GetTree().CallGroup("Konpeito", Node.MethodName.QueueFree);
    }

}
