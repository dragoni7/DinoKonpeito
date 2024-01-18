using Godot;

public partial class DifficultyTracker : Node
{
    public static int Stage { get; private set; }

    [Signal]
    public delegate void DifficultyChangedEventHandler(int stage);

    public DifficultyTracker()
    {
        Stage = 0;
    }

    public void NextStage()
    {
        if (Stage != GameConsts.Difficulty.MaxDifficultyStages)
        {
            Stage++;
            EmitSignal(SignalName.DifficultyChanged, Stage);
        }
    }
}
