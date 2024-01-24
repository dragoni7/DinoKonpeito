using Godot;

public partial class PauseScreen : CanvasLayer
{
    public override void _Ready()
    {
        GetNode<Button>("VBoxContainer/ContinueButton").Pressed += OnContinueButtonPress;
        GetNode<Button>("VBoxContainer/OptionsButton").Pressed += OnOptionsButtonPress;
        GetNode<Button>("VBoxContainer/MenuButton").Pressed += OnMenuButtonPress;
    }

    public void OnContinueButtonPress()
    {
        Hide();
        GetTree().Paused = false; 
    }

    public void OnOptionsButtonPress()
    {

    }

    public void OnMenuButtonPress()
    {
        GetTree().Paused = false;
        SceneLoader.GetInstance(this).ChangeToScene("UI/Menu.tscn");
    }
}
