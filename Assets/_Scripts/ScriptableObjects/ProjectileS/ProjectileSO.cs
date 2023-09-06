using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ProjectileSO : ScriptableObject
{
    [field:SerializeField] public float baseDamage {  get; private set; }
    [field: SerializeField] public float baseTravelSpeed { get; private set; }
    [field: SerializeField] public float baseMaxRange { get; private set; }
    [field: SerializeField] public float baseEnergyGain { get; private set; }
}
