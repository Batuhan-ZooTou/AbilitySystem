using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "AbilityEffectScriptableObject/DamageOverTimeEffectSO")]
public class DamageOverTimeEffectSO : AbilityEffectSO
{
    public int tickCount;
    public float tickDamage;
}
