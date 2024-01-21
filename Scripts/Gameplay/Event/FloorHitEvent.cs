
public class FloorHitEvent : IEvent
{
    public Floor HitFloor { get; private set; }
    public FloorHitEvent(Floor hitFloor)
    {
        HitFloor = hitFloor;
    }
}
