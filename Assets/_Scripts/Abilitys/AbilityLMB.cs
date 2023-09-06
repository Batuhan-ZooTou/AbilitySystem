using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityLMB : Ability
{
    public override void Cast()
    {
        base.Cast();
        ProjectilePool.Instance.projectileLMBPool.Get();
    }
}
