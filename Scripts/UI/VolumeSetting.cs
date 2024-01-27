using Godot;

[Tool]
public partial class VolumeSetting : HBoxContainer
{

    private string _busName = "<Bus Name>";

    private Label _busNameLabel;
    private HSlider _volumeSlider;
    private Label _volumePercentageLabel;


    [Export]
    public string BusName
    {
        get => _busName;
        private set
        {
            _busName = value;
            UpdateBusNameLabel();
        }
    }

    [Export]
    public int BusIndex { get; private set; }

    public override void _Ready()
    {
        _busNameLabel = GetNode<Label>("BusNameLabel");
        UpdateBusNameLabel();

        _volumeSlider = GetNode<HSlider>("VolumeHSlider");
        _volumeSlider.ValueChanged += OnVolumeValueChanged;

        _volumePercentageLabel = GetNode<Label>("VolumePercentageLabel");
        UpdateVolumePercentageLabel();
    }

    public void UpdateSetting(double value)
    {
        _volumeSlider.Value = value;
        float decibels = SoundUtil.PercentageToDecibels(value);
        AudioServer.SetBusVolumeDb(BusIndex, decibels);
    }

    public double GetValue()
    {
        return _volumeSlider.Value;
    }

    private void OnVolumeValueChanged(double value)
    {
        UpdateVolumePercentageLabel();

        if (value == 0)
        {
            AudioServer.SetBusMute(BusIndex, true);
            return;
        }

        if (AudioServer.IsBusMute(BusIndex))
        {
            AudioServer.SetBusMute(BusIndex, false);
        }

        float decibels = SoundUtil.PercentageToDecibels(value);
        AudioServer.SetBusVolumeDb(BusIndex, decibels);
    }

    private void UpdateBusNameLabel()
    {
        if (_busNameLabel != null)
        {
            _busNameLabel.Text = _busName;
        }
    }

    private void UpdateVolumePercentageLabel()
    {
        double value = _volumeSlider.Value;
        string volumePercentage = $"{Mathf.Floor(value)}%";
        _volumePercentageLabel.Text = volumePercentage;
    }
}
