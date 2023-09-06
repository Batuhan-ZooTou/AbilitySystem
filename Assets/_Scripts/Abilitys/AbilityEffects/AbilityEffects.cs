using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum AbilityEffectTypes
{
    Stun,
    Incapacitate,
    Petrify,
    Panic,
    Slow,
    FadingSlow,
    Silence,
    Weaken,
    Blind,
    Broken,
    DamageOverTime,
    Root,
    None,

}
[Serializable]
public class AbilityEffects 
{
    public float effectDuration;
    //public Sprite abilityIcon;
    public string effectType;
    public virtual void OnApply(CoreCombat hit) { }
    public virtual void DuringEffect(CoreCombat hit) { }
    public virtual void OnClear(CoreCombat hit) { }

}

