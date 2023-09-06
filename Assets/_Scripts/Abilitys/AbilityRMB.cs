using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AbilityRMB : Ability
{
    [SerializeField] private int shotsCount;
    [SerializeField] private float shotDelay;
    private bool channelStoped;
    public override void Trigger()
    {
        base.Trigger();
        caster.abilityCastRangeIndicator.transform.localScale = new Vector3(abilitySO.castRange, caster.abilityCastRangeIndicator.transform.localScale.y, caster.abilityCastRangeIndicator.transform.localScale.z);
        caster.abilityCastRangeIndicator.SetActive(true);
        channelStoped = false;

    }
    public override void Cast()
    {
        base.Cast();
        ChangeAbilityState(AbilityState.Channeling);
        StartCoroutine(DelayAfterEachShot());
    }
    public override void CanceledChannel()
    {
        base.CanceledChannel();
        channelStoped = true;
    }
    private IEnumerator DelayAfterEachShot()
    {
        for (int i = 0; i < shotsCount; i++)
        {
            if (channelStoped)
            {
                i = shotsCount;
                caster.abilityCastRangeIndicator.SetActive(false);
                ChangeAbilityState(AbilityState.OnCooldown);
                channelStoped = false;
                StopCoroutine(DelayAfterEachShot());
            }
            else
            {
                ProjectilePool.Instance.projectileRMBPool.Get();
            }
            if (i== shotsCount-1)
            {
                caster.abilityCastRangeIndicator.SetActive(false);
                ChangeAbilityState(AbilityState.OnCooldown);
            }
            yield return new WaitForSeconds(shotDelay);
        }
    }
    
}
