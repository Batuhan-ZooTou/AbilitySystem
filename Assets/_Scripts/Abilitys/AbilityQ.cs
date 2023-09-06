using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityQ : Ability
{
    public GameObject prefab;
    public float radius;
    public float duration;
    public override void Trigger()
    {
        base.Trigger();
        caster.aoeAbilityIndicator.transform.localScale = Vector3.one;
        caster.aoeAbilityIndicator.transform.position = caster.playerCamera.skillPoint;
        caster.aoeAbilityIndicator.SetActive(true);
        caster.aoeAbilityIndicator.transform.localScale = Vector3.one*prefab.GetComponent<AreaOfEffect>().areaOfEffectSO.radiusOfArea/2;
    }
    public override void Cancel()
    {
        base.Cancel();
        caster.aoeAbilityIndicator.SetActive(false);

    }
    public override void Casting()
    {
        base.Casting();
        caster.aoeAbilityIndicator.transform.position = caster.playerCamera.skillPoint;
    }
    public override void Cast()
    {
        base.Cast();
        GameObject aoeAbility=Instantiate(prefab,caster.aoeAbilityIndicator.transform.position, caster.aoeAbilityIndicator.transform.rotation,null);
        aoeAbility.transform.localScale= Vector3.one * radius / 2;
        aoeAbility.GetComponent<AreaOfEffect>().ability = this;
        caster.aoeAbilityIndicator.SetActive(false);
    }
    public override void Effect(CoreHealth enemy)
    {
        enemy.TakeDamage(15);
        foreach (AbilityEffectDataHolder ability in abilityEffectData)
        {
            enemy.ApplyEffect(ability.abilityEffect, ability.effectType, ability.AbilityEffectSO);
        }

    }
}
