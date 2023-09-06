using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class DamagePerSecond : AbilityEffects
{
    public int tickCount;
    public float tickDamage;
    public DamagePerSecond(DamageOverTimeEffectSO _damageOverTimeEffectSO)
    {
        tickDamage = _damageOverTimeEffectSO.tickDamage;
        tickCount = _damageOverTimeEffectSO.tickCount;
        effectDuration = _damageOverTimeEffectSO.effectDuration;
    }
    public override void OnApply(CoreCombat hit)
    {
        base.OnApply(hit);
        hit.TryGetComponent(out CoreHealth coreHealth);
        coreHealth.TakeDotDamage(tickDamage,tickCount,effectDuration/tickCount);
    }
}
