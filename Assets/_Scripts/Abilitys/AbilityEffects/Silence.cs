using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class Silence : AbilityEffects
{
    public Silence(SilenceEffectSO _silenceEffectSO)
    {
        effectDuration = _silenceEffectSO.effectDuration;

    }
    public override void OnApply(CoreCombat hit)
    {
        base.OnApply(hit);
        hit.CoreMovement.GetSilenced(true);
    }
    public override void OnClear(CoreCombat hit)
    {
        base.OnClear(hit);
        hit.CoreMovement.GetSilenced(false);

    }
}
