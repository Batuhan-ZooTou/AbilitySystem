using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class PlayerHealth : CoreHealth
{
    public List<GameObject> effectVisualPrefab;
    public override void ApplyEffect(AbilityEffects effect, int buffType, AbilityEffectSO abilityEffectSO)
    {
        base.ApplyEffect(effect, buffType, abilityEffectSO);
        AbilityEffects newInstance = effect.DeepClone();
        foreach (GameObject item in effectVisualPrefab)
        {
            if (!item.activeSelf)
            {
                item.SetActive(true);
                item.transform.GetChild(2).GetComponent<Image>().sprite = abilityEffectSO.effectIcon;
                item.GetComponent<Slider>().DOValue(0, newInstance.effectDuration).OnComplete(() => item.GetComponent<Slider>().DOValue(1, 0).OnComplete(() => item.SetActive(false)));
                break;
            }
        }
        newInstance.OnApply(CoreCombat);
        AbilityEffectSlot addedEffect = new AbilityEffectSlot(newInstance, newInstance.effectDuration);
        if (buffType==1)
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
