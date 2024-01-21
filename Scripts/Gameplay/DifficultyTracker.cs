using Godot;

public partial class DifficultyTracker : Node
{
    public static int Stage { get; private set; }

    public DifficultyTracker()
    {
        Stage = 0;
    }

    public void NextStage()
    {
        if (Stage != GameConsts.Difficulty.MaxDifficultyStages)
        {
            Stage++;
            EventBus.Instance.Raise(new DifficultyChangeEvent(Stage));
        }
    }
}
