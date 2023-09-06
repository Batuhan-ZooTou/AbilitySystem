using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class FadingSlow : AbilityEffects
{
    public float slowedAmount { get; private set; }
    public FadingSlow(FadingSlowEffectSO _fadingSlowEffectSO)
    {
        effectDuration = _fadingSlowEffectSO.effectDuration;
    }
    public override void OnApply(CoreCombat hit)
    {
        base.OnApply(hit);
        slowedAmount = hit.CoreMovement.ChangeMoveSpeedInstant(-100);
        hit.CoreMovement.RegainMoveSpeedOvertime(slowedAmount, effectDuration);

    }
}
