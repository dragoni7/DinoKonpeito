
public class ScoreChangeEvent : IEvent
{
    public int NewScore { get; private set; }
    public ScoreChangeEvent(int newScore)
    {
        NewScore = newScore;
    }
}
