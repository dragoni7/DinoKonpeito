using Godot;
using Godot.Collections;

partial class KonpeitoManager : Node, IManagerNode<KonpeitoManager>
{
    [Export]
    public PackedScene KonpeitoScene;

    [Export]
    public PackedScene WhiteKonpeitoScene;

    [Export]
    public PackedScene YellowKonpeitoScene;

    [Export]
    public PackedScene FlashingKonpeitoScene;

    [Signal]
    public delegate void IncreaseScoreEventHandler(int amount);

    [Signal]
    public delegate void DisplayScoreTextEventHandler(int amount, Vector2 position);

    public float SpeedModifier { get; set; }

    public static KonpeitoManager GetInstance(Node from)
    {
        return from.GetNode<KonpeitoManager>("/root/Game/KonpeitoManager");
    }

    public override void _Ready()
    {
        SpeedModifier = 1;
    }

    public override void _PhysicsProcess(double delta)
    {
        GetTree().CallGroup("Konpeito", Konpeito.MethodName.Move, SpeedModifier);
    }

    public void OnSpawnKonpeito()
    {
        var spawnLocation = GetNode<PathFollow2D>("Path2D/SpawnPoint");

        SpawnKonpeito(spawnLocation);

        double doubleSpawnChance = DifficultyTracker.Stage / 4D;

        if (doubleSpawnChance > GD.RandRange(0, 1.5D))
        {
            GD.Print("double spawn");
            SpawnKonpeito(spawnLocation);
        }
    }

    private void SpawnKonpeito(PathFollow2D location)
    {
        location.ProgressRatio = GD.Randf();

        Konpeito konpeito = PickScene().Instantiate<Konpeito>();
        konpeito.Destroyed += OnKonpeitoDestroyed;

        konpeito.Position = location.Position;
        konpeito.Speed += (float)GD.RandRange(0.25f, 0.75f) + (DifficultyTracker.Stage * 0.4f);

        AddChild(konpeito);
    }


    private PackedScene PickScene()
    {
        PackedScene sceneToUse = KonpeitoScene;

        double specialKonpeitoChance = (DifficultyTracker.Stage + 1) / 16D;

        if (specialKonpeitoChance > GD.RandRange(0, 1D))
        {
            float rand = GD.Randf();

            switch (rand)
            {
                case > 0.5f:
                    {
                        return WhiteKonpeitoScene;
                    }
                case > 0.2f:
                    {
                        return YellowKonpeitoScene;
                    }
                default:
                    {
                        return FlashingKonpeitoScene;
                    }
            }
        }

        return sceneToUse;
    }

    public void OnKonpeitoDestroyed(Konpeito konpeito, Array<StringName> groups)
    {
        bool noPoints = groups.Contains("Floor");

        if (!noPoints)
        {
            KonpeitoEffect effect = konpeito.Effect;
            effect.Reparent(this);
            effect.Execute();

            int score = GetKonpeitoScore(konpeito.Position);

            EmitKonpeitoScore(konpeito, score);
        }

        konpeito.QueueFree();
    }

    public void HitAllKonpeito()
    {
        GD.Print("hit all konpeito");
        var konpeitos = GetTree().GetNodesInGroup("Konpeito");
        
        foreach (Konpeito k in konpeitos)
        {
            EmitKonpeitoScore(k, 10);
        }

        GetTree().CallGroup("Konpeito", Node.MethodName.QueueFree);
    }

    private int GetKonpeitoScore(Vector2 pos)
    {
        float distance = pos.Y;

        switch(distance) {

            case < 200:
                {
                    return 1000;
                }
            case < 400:
                {
                    return 300;
                }
            case < 600:
                {
                    return 100;
                }
            case < 800:
                {
                    return 50;
                }
            default:
                {
                    return 10;
                }
        }
    }

    private void EmitKonpeitoScore(Konpeito konpeito, int score)
    {
        EmitSignal(SignalName.IncreaseScore, score);
        EmitSignal(SignalName.DisplayScoreText, score, konpeito.Position);
    }

}
