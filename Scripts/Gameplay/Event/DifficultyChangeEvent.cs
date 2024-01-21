
public class DifficultyChangeEvent : IEvent
{
    public int NewDifficulty { get; private set; }

    public DifficultyChangeEvent(int newDifficulty)
    {
        NewDifficulty = newDifficulty;
    }
}
