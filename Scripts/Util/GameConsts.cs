using Godot;

public static class GameConsts
{
    public static class Konpeito
    {
        public const float RestoringChance = 0.3f;

        public const float SlowingChance = 0.2f;

        public const float SuperChance = 0.1f;

        public const float SpecialChance = 0.05f;

        public const float SpeedAddMin = 0.25f;

        public const float SpeedAddMax = 0.75f;

        public const float SpeedPercentPerStage = 0.35f;

        public const float DoubleSpawnChance = 0.25f;

        public const float MinSpawnTime = 0.1f;

        public const float MaxSpawnTime = 3f;

        public const float SpawnTimeReduction = 0.5f;
    }

    public static class Difficulty
    {
        public const int MaxDifficultyStages = 9;

        public const int StageInterval = 5000;
    }

    public enum Scores
    {
        Highest = 1000,
        High = 300,
        Mid = 100,
        Low = 50,
        Lowest = 10
    }

    public static class ScoreColors
    {
        public static Color Get(Scores score)
        {
            switch (score)
            {
                case Scores.Highest:
                    {
                        return Colors.White;
                    }
                case Scores.High:
                    {
                        return Colors.White;
                    }
                case Scores.Mid:
                    {
                        return Colors.WhiteSmoke;
                    }
                case Scores.Low:
                    {
                        return Colors.Blue;
                    }
                default:
                    {
                        return Colors.Red;
                    }
            }
        }
    }
}
