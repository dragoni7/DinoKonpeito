using Godot;

public partial class OptionsScreen : VBoxContainer
{
    private void OnBackButtonPressed()
    {
        SceneLoader.GetInstance(this).ChangeToScene("UI/Menu");
    }

    private void OnClearDataPressed()
    {
        GetNode<ConfirmationDialog>("ConfirmationDialog").Show();
        ZIndex = -1;
    }

    private void OnClearDataConfirm()
    {
        GameData.Instance.Reset();
        ZIndex = 0;
    }

    private void OnClearDataCancel()
    {
        ZIndex = 0;
    }
}
