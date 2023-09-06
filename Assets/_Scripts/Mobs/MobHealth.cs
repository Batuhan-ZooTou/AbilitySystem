using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class MobHealth : CoreHealth
{
    public override void ApplyEffect(AbilityEffects effect, int buffType, AbilityEffectSO abilityEffectSO)
    {
        base.ApplyEffect(effect, buffType, abilityEffectSO);
        AbilityEffects newInstance = effect.DeepClone();
        newInstance.OnApply(CoreCombat);
        AbilityEffectSlot addedEffect = new AbilityEffectSlot(newInstance, newInstance.effectDuration);
        if (buffType == 1)
        {
            debuffSlots.Add(addedEffect);
        }
        else if (buffType == 0)
        {
            buffSlots.Add(addedEffect);
        }
        StartCoroutine(ClearEffect(addedEffect, buffType));
    }
}
