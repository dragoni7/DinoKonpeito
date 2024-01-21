using Godot;

public partial class KonpeitoSpawner : Node
{
    [Export]
    public PackedScene KonpeitoScene;

    [Export]
    public PackedScene RestoringKonpeitoScene;

    [Export]
    public PackedScene SlowingKonpeitoScene;

    [Export]
    public PackedScene SuperKonpeitoScene;

    public bool CanSpawn { get; set; } = true;

    public void OnSpawnKonpeito()
    {
        if (CanSpawn)
        {
            var spawnLocation = GetNode<PathFollow2D>("Path2D/SpawnPoint");

            KonpeitoManager.GetInstance(this).AddChild(BuildKonpeito(spawnLocation));

            double doubleSpawnChance = DifficultyTracker.Stage * GameConsts.Konpeito.DoubleSpawnChance;

            if (doubleSpawnChance > GD.RandRange(0, 1.5D))
            {
                KonpeitoManager.GetInstance(this).AddChild(BuildKonpeito(spawnLocation));
            }
        }
    }

    private Konpeito BuildKonpeito(PathFollow2D location)
    {
        location.ProgressRatio = GD.Randf();

        Konpeito konpeito = PickScene().Instantiate<Konpeito>();

        konpeito.Position = location.Position;
        konpeito.Speed += (float)GD.RandRange(GameConsts.Konpeito.SpeedAddMin, GameConsts.Konpeito.SpeedAddMax) + (DifficultyTracker.Stage * GameConsts.Konpeito.SpeedPercentPerStage);

        return konpeito;
    }

    private PackedScene PickScene()
    {
        PackedScene sceneToUse = KonpeitoScene;

        double specialKonpeitoChance = (DifficultyTracker.Stage + 1) * GameConsts.Konpeito.SpecialChance;

        if (specialKonpeitoChance > GD.RandRange(0, 1D))
        {
            float rand = GD.Randf();

            switch (rand)
            {
                case < GameConsts.Konpeito.SuperChance:
                    {
                        return SuperKonpeitoScene;
                    }
                case < GameConsts.Konpeito.SlowingChance:
                    {
                        return SlowingKonpeitoScene;
                    }
                case < GameConsts.Konpeito.RestoringChance:
                    {
                        return RestoringKonpeitoScene;
                    }
            }
        }

        return sceneToUse;
    }
}
