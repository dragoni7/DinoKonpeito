using Godot;

public partial class OptionsScreen : VBoxContainer
{
    public override void _Ready()
    {
        GameData gameData = GameData.Instance;
        GetNode<VolumeSetting>("VolumeSettings/MusicSetting").UpdateSetting(gameData.MusicVolume);
        GetNode<VolumeSetting>("VolumeSettings/SFXSetting").UpdateSetting(gameData.SFXVolume);
    }

    private void OnBackButtonPressed()
    {
        GameData gameData = GameData.Instance;
        gameData.MusicVolume = GetNode<VolumeSetting>("VolumeSettings/MusicSetting").GetValue();
        gameData.SFXVolume = GetNode<VolumeSetting>("VolumeSettings/SFXSetting").GetValue();
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
