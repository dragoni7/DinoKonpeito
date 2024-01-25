using Godot.Collections;
using Godot;

public partial class KonpeitoCoin : Konpeito
{
    public new void OnHit(Array<StringName> groups)
    {
        EventBus.Instance.Raise(new KonpeitoHitEvent(this, groups, (int) GameConsts.Scores.Highest));
    }
}
