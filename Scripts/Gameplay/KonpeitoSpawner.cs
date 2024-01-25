using Godot;
using Godot.Collections;
using System.Linq;

public partial class KonpeitoSpawner : Node
{
    [Export]
    Array<RandomKonpeito> SpecialKonpeitos;

    public bool CanSpawn { get; set; } = true;

    public override void _Ready()
    {
        EventBus eventBus = EventBus.Instance;
        eventBus.Subscribe<DifficultyChangeEvent>(OnDifficultyChanged);
        eventBus.Subscribe<GameOverEvent>(OnGameOver);

        GD.Print(SpecialKonpeitos[0].Name);
    }

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

        Konpeito konpeito = PickRandomKonpeito().Instantiate<Konpeito>();

        konpeito.Position = location.Position;
        konpeito.Speed += (float)GD.RandRange(GameConsts.Konpeito.SpeedAddMin, GameConsts.Konpeito.SpeedAddMax) + (DifficultyTracker.Stage * GameConsts.Konpeito.SpeedPercentPerStage);

        return konpeito;
    }

    private PackedScene PickRandomKonpeito()
    {
        PackedScene chosenScene = SpecialKonpeitos.First(d => d.Name == "Konpeito").KonpeitoScene;

        if (SpecialKonpeitos.Count > 0)
        {
            int overallChance = 0;

            foreach (RandomKonpeito konpeitoData in SpecialKonpeitos)
            {
                overallChance += konpeitoData.PickChance;
            }

            var rand = GD.Randi() % overallChance;
            int offset = 0;

            foreach (RandomKonpeito konpeitoData in SpecialKonpeitos)
            {
                if (rand < konpeitoData.PickChance + offset)
                {
                    if (konpeitoData.DefaultPicked)
                    {
                        chosenScene = konpeitoData.KonpeitoScene;
                    }

                    break;
                }
                else
                {
                    offset += konpeitoData.PickChance;
                }
            }
        }

        return chosenScene;
    }

    private void OnDifficultyChanged(DifficultyChangeEvent e)
    {
        switch (e.NewDifficulty)
        {
            case 1:
                {
                    SpecialKonpeitos.First(d => d.Name == "RestoringKonpeito").DefaultPicked = true;
                    GD.Print("Restoring enabled");
                    break;
                }
            case 3:
                {
                    SpecialKonpeitos.First(d => d.Name == "SlowingKonpeito").DefaultPicked = true;
                    GD.Print("Slowing enabled");
                    break;
                }
            case 4:
                {
                    SpecialKonpeitos.First(d => d.Name == "SuperKonpeito").DefaultPicked = true;
                    GD.Print("Super enabled");
                    break;
                }
        }
    }

    private void OnGameOver(GameOverEvent e)
    {
        SpecialKonpeitos.First(d => d.Name == "RestoringKonpeito").DefaultPicked = false;
        SpecialKonpeitos.First(d => d.Name == "SlowingKonpeito").DefaultPicked = false;
        SpecialKonpeitos.First(d => d.Name == "SuperKonpeito").DefaultPicked = false;
    }
}
