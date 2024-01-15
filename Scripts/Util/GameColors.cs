using Godot;

public static class GameColors
{
    public static Color FromScore(int score)
    {
        switch (score)
        {
            case 1000:
                {
                    return Colors.White;
                }
            case 300:
                {
                    return Colors.White;
                }
            case 100:
                {
                    return Colors.WhiteSmoke;
                }
            case 50:
                {
                    return Colors.Blue;
                }
            default:
                {
                    return Colors.Red;
                }
        }
    }

    public static Color ScoreFlashColor(int score)
    {
        return score == 1000 ? Colors.Gold : Colors.Blue;
    }
}
