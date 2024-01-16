using Godot;

public partial class DifficultyTracker : Node
{
    public static int Stage { get; private set; }

    [Signal]
    public delegate void DifficultyChangedEventHandler(int stage);

    private const int MaxStage = 9;

    public DifficultyTracker()
    {
        Stage = 0;
    }

    public void NextStage()
    {
        if (Stage != MaxStage)
        {
            Stage++;
            EmitSignal(SignalName.DifficultyChanged, Stage);
        }
    }
}
