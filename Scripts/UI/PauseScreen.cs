using Godot;

public partial class PauseScreen : CanvasLayer
{
    public override void _Ready()
    {
        GetNode<Button>("VBoxContainer/ContinueButton").Pressed += OnContinueButtonPress;
        GetNode<Button>("VBoxContainer/MenuButton").Pressed += OnMenuButtonPress;
    }

    public void OnContinueButtonPress()
    {
        Hide();
        GetTree().Paused = false; 
    }

    public void OnMenuButtonPress()
    {
        Hide();
        GetTree().Paused = false;
        GameStateManager.GetInstance(this).ChangeToState(GameState.ExitingGame);

    }
}
