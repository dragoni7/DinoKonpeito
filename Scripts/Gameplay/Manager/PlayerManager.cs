using DinoKonpeito.Component;
using Godot;

partial class PlayerManager : Node, ISingleInstance<PlayerManager>
{
    [Export]
    private PackedScene _playerScene;

    public Player Player { get; private set; }

    public Marker2D PlayerSpawnPoint { get; private set; }

    public static PlayerManager GetInstance(Node from)
    {
        return from.GetNode<PlayerManager>("/root/Game/PlayerManager");
    }

    public override void _Ready()
    {
        PlayerSpawnPoint = GetNode<Marker2D>("StartPosition");

        SubscribeEvents();
        SpawnPlayer(PlayerSpawnPoint.Position);
    }

    public void SubscribeEvents()
    {
        EventBus eventBus = EventBus.Instance;

        eventBus.Subscribe<DifficultyChangeEvent>(OnDifficultyIncreased);
    }

    public void SpawnPlayer(Vector2 position)
    {
        Player = _playerScene.Instantiate<Player>();
        Player.Position = position;
        AddChild(Player);
    }

    public void DespawnPlayer()
    {
        Player.QueueFree();
    }

    public void OnDifficultyIncreased(DifficultyChangeEvent e)
    {
        Player.GetNode<PlayerMovementComponent>("PlayerMovementComponent").Speed += GameConsts.Player.DifficultySpeedIncrease;
        Player.GetNode<HeadComponent>("HeadComponent").Step *= GameConsts.Player.DifficultyHeadStepMultiplier;
    }
}
