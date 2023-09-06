using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class Stun : AbilityEffects
{
    public Stun(StunEffectSO _stunEffectSO)
    {
        effectDuration = _stunEffectSO.effectDuration;
    }
    public override void OnApply(CoreCombat hit)
    {
        base.OnApply(hit);
        hit.CoreMovement.GetStunned(true);
    }
    public override void DuringEffect(CoreCombat hit)
    {
        base.DuringEffect(hit);
        Debug.Log("hit");
    }
    public override void OnClear(CoreCombat hit)
    {
        base.OnClear(hit);
        hit.CoreMovement.GetStunned(false);

    }
}
