using Godot;

public partial class DifficultyTracker : Node
{
    private DifficultyStage _stage;

    [Signal]
    public delegate void DifficultyChangedEventHandler(DifficultyStage stage);

    public DifficultyTracker()
    {
        _stage = DifficultyStage.One;
    }

    public void NextStage()
    {
        if (_stage != DifficultyStage.Ten)
        {
            _stage++;
            EmitSignal(SignalName.DifficultyChanged, (int)_stage);
        }
    }
}

public enum DifficultyStage
{
    One,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten
}
