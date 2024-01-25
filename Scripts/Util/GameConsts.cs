using Godot;

public static class GameConsts
{
    public static class Konpeito
    {
        public const float SpeedAddMin = 0.25f;

        public const float SpeedAddMax = 1.2f;

        public const float SpeedPercentPerStage = 0.45f;

        public const float DoubleSpawnChance = 0.3f;

        public const float MinSpawnTime = 0.1f;

        public const float MaxSpawnTime = 3f;

        public const float SpawnTimeReduction = 0.5f;
    }

    public static class Player
    {
        public const float BaseSpeed = 260f;

        public const float DifficultySpeedIncrease = 30f;

        public const float BaseHeadStepX = -12f;

        public const float BaseHeadStepY = -12f;

        public const float DifficultyHeadStepMultiplier = 1.05f;
    }

    public static class Difficulty
    {
        public const int MaxDifficultyStages = 29;

        public const int StageInterval = 300;
    }

    public static class Bounds
    {
        public const int GameAreaX = 1856;
    }

    public enum Scores
    {
        Highest = 1000,
        High = 400,
        Mid = 200,
        Low = 50,
        Lower = 25,
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
                        return Colors.Gold;
                    }
                case Scores.High:
                    {
                        return Colors.Violet;
                    }
                case Scores.Mid:
                    {
                        return Colors.White;
                    }
                case Scores.Low:
                    {
                        return Colors.Blue;
                    }
                case Scores.Lower:
                    {
                        return Colors.Green;
                    }
                default:
                    {
                        return Colors.Red;
                    }
            }
        }
    }
}
