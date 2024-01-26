
public class GameDataUpdatedEvent : IEvent
{
    public int HighScore { get; private set; }

    public GameDataUpdatedEvent(int highScore)
    {
        HighScore = highScore;
    }
}
