using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AbilityEffectSlot 
{
    public AbilityEffectSlot(AbilityEffects _type,float _duration)
    {
        abilityEffectType = _type;
        currentDuration = _duration;
    }
    public AbilityEffects abilityEffectType;
    public float currentDuration;
    public bool isPurged=false;
    
}
