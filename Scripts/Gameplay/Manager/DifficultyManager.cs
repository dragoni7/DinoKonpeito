using Godot;

public partial class DifficultyManager : Node, ISingleInstance<DifficultyManager>
{
    public int Stage { get; private set; } = 0;

    public static DifficultyManager GetInstance(Node from)
    {
        return from.GetNode<DifficultyManager>("/root/Game/DifficultyManager");
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
