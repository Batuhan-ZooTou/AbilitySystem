using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu]
public class AreaOfEffectSO : ScriptableObject
{
    [field: SerializeField] public float radiusOfArea { get; private set; }
    [field: SerializeField] public float castRange { get; private set; }
    [field: SerializeField] public float timingCircleTime { get; private set; }
    [field: SerializeField] public float liveTime { get; private set; }
}
