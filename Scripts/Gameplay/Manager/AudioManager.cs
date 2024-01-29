using Godot;

public partial class AudioManager : Node, ISingleInstance<AudioManager>
{
    private AudioStreamPlayer _hitAudioPlayer;

    private AudioStreamPlayer _stepAudioPlayer;

    public static AudioManager GetInstance(Node from)
    {
        return from.GetNode<AudioManager>("/root/AudioManager");
    }

    public override void _Ready()
    {
        _hitAudioPlayer = GetNode<AudioStreamPlayer>("HitAudioPlayer");
        _stepAudioPlayer = GetNode<AudioStreamPlayer>("StepAudioPlayer");

        SubscribeEvents();
    }

    public void SubscribeEvents()
    {
        EventBus eventBus = EventBus.Instance;
        eventBus.Subscribe<KonpeitoHitEvent>(OnKonpeitoHit);
        eventBus.Subscribe<PlayerPositionChangedEvent>(OnPlayerPositionChanged);
    }

    private void OnKonpeitoHit(KonpeitoHitEvent e)
    {
        if (!e.GroupsHit.Contains("Floor"))
        {
            _hitAudioPlayer.PitchScale = (float)GD.RandRange(0.8f, 1.2f);
            _hitAudioPlayer.Play();
        }
    }

    private void OnPlayerPositionChanged(PlayerPositionChangedEvent e)
    {
        if (!_stepAudioPlayer.Playing)
        {
            _stepAudioPlayer.Play();
        }
    }
}
