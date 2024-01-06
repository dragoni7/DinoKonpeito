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

    [Export]
    public PackedScene WhiteKonpeitoScene { get; set; }

    [Export]
    public PackedScene YellowKonpeitoScene { get; set; }

    [Signal]
    public delegate void IncreaseScoreEventHandler(int amount);

    private float _spawns;

    public float Spawns => _spawns;

    private float _speedModifier;

    public float SpeedModifier
    {
        get => _speedModifier;
        set => _speedModifier = value;
    }

    public override void _Ready()
    {
        _speedModifier = 1;
    }

    public override void _Process(double delta)
    {
        GetTree().CallGroup("Konpeito", Konpeito.MethodName.Move);
    }

    public void SpawnKonpeito()
    {
        PackedScene sceneToUse = KonpeitoScene;

        if (GD.RandRange(1, 3) % 3 == 0)
        {
            if (GD.RandRange(1, 2) % 2 == 0)
            {
                sceneToUse = WhiteKonpeitoScene;
            }
            else
            {
                sceneToUse = YellowKonpeitoScene;
            }
        }

        Konpeito konpeito = sceneToUse.Instantiate<Konpeito>();
        konpeito.Destroyed += OnKonpeitoDestroyed;

        var spawnLocation = GetNode<PathFollow2D>("Path2D/SpawnPoint");
        spawnLocation.ProgressRatio = GD.Randf();

        konpeito.Position = spawnLocation.Position;
        konpeito.CurrentSpeed += (float)GD.RandRange(0.25f, 1.0f) + (_spawns * 0.03f);
        konpeito.ModifySpeed(_speedModifier);

        AddChild(konpeito);
        _spawns++;
    }

    public void OnKonpeitoDestroyed(Konpeito konpeito, bool hitFloor)
    {
        if (!hitFloor)
        {
            KonpeitoEffect effect = konpeito.Effect;
            effect.Reparent(this);
            effect.Execute();
            EmitSignal(SignalName.IncreaseScore, konpeito.Score);
        }

        konpeito.QueueFree();
    }

    public void ClearKonpeito()
    {
        GetTree().CallGroup("Konpeito", Node.MethodName.QueueFree);
    }

}
