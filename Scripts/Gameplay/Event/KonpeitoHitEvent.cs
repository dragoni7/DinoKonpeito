
using Godot.Collections;
using Godot;

public class KonpeitoHitEvent : IEvent
{
    public Konpeito KonpeitoHit { get; private set; }

    public Array<StringName> GroupsHit { get; private set; }

    public int ScoreOnHit { get; private set; }

    public KonpeitoHitEvent(Konpeito konpeitoHit, Array<StringName> groupsHit) : this(konpeitoHit, groupsHit, ScoreHelper.GetKonpeitoScore(konpeitoHit.Position)) { }

    public KonpeitoHitEvent(Konpeito konpeitoHit, Array<StringName> groupsHit, int score)
    {
        KonpeitoHit = konpeitoHit;
        GroupsHit = groupsHit;
        ScoreOnHit = score;
    }
}
