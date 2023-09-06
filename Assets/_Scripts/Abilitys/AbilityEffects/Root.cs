using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class Root : AbilityEffects
{
    public Root(RootEffectSO _rootEffectSO)
    {
        effectDuration = _rootEffectSO.effectDuration;

    }
    public override void OnApply(CoreCombat hit)
    {
        base.OnApply(hit);
        hit.CoreMovement.GetRooted(true);
    }
    public override void OnClear(CoreCombat hit)
    {
        base.OnClear(hit);
        hit.CoreMovement.GetRooted(false);
    }
}
