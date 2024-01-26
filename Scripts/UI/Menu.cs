using Godot;

public partial class Menu : Node
{
    public override void _Ready()
    {
		GameData.Instance.ReadData();
    }

    public void OnStartButtonPressed()
	{
		GameStateManager.GetInstance(this).ChangeToState(GameState.Playing);
	}

	public void OnOptionsButtonPressed()
	{
        SceneLoader.GetInstance(this).ChangeToScene("UI/OptionsScreen");
    }

	public void OnQuitButtonPressed()
	{
		GameData.Instance.SaveData();
		GetTree().Quit();
	}
}
