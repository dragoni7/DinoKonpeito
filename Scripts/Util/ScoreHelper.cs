using Godot;

public static class ScoreHelper
{
    public static int GetKonpeitoScore(Vector2 pos)
    {
        float distance = pos.Y;

        switch (distance)
        {

            case < 250:
                {
                    return (int)GameConsts.Scores.Highest;
                }
            case < 450:
                {
                    return (int)GameConsts.Scores.High;
                }
            case < 600:
                {
                    return (int)GameConsts.Scores.Mid;
                }
            case < 800:
                {
                    return (int)GameConsts.Scores.Low;
                }
            default:
                {
                    return (int)GameConsts.Scores.Lowest;
                }
        }
    }
}
