using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class Slow : AbilityEffects
{
    public float slowPercentage;
    public float slowedAmount { get; private set; }
    public Slow(SlowEffectSO _slowEffectSO)
    {
        slowPercentage = _slowEffectSO.slowPercentage;
        effectDuration = _slowEffectSO.effectDuration;
    }
    public override void OnApply(CoreCombat hit)
    {
        base.OnApply(hit);
        slowedAmount=hit.CoreMovement.ChangeMoveSpeedInstant(-slowPercentage);
        Debug.Log(slowedAmount);

    }
    public override void DuringEffect(CoreCombat hit)
    {
        base.DuringEffect(hit);
        Debug.Log(slowedAmount);
    }
    public override void OnClear(CoreCombat hit)
    {
        base.OnClear(hit);
        Debug.Log(slowedAmount);
        hit.CoreMovement.RegainMoveSpeedInstant(+slowedAmount);

    }
}
