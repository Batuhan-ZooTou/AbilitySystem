using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityState
{
    SetReady,
    Ready,
    Triggered,
    Casting,
    Canceled,
    Casted,
    Channeling,
    CanceledChanneling,
    OnCooldown,
}
[CreateAssetMenu]
public class PlayerAbilitySO : ScriptableObject
{
    public string abilityName;
    public Sprite icon;
    public string description;
    public float cooldownTime;
    public float cooldownTimeAfterCancel;
    public float castTime;
    public float useTime;
    public float energyCost;
    public bool cooldownWhileActive;
    public float castRange;
    [Range(0, 100)] public int slowPercentageWhileCasting; 
    [HideInInspector]public float slowAmountWhileCasting;

}
