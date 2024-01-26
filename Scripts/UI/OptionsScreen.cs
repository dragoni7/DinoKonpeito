using Godot;
using System;

public partial class OptionsScreen : VBoxContainer
{
    private void OnBackButtonPressed()
    {
        SceneLoader.GetInstance(this).ChangeToScene("UI/Menu");
    }

    private void OnClearDataPressed()
    {
        GameData.Instance.Reset();
    }
}
