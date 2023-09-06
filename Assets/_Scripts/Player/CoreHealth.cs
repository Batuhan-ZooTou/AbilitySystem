using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public abstract class CoreHealth : MonoBehaviour
{
    [field:SerializeField] public float health { get ; set ; }
    [field: SerializeField] public bool isDead { get ; set; }
    [field: SerializeField] public bool isInvincible { get ; set; }
    [field: SerializeField] public Slider healthBar { get; set; }
    [field: SerializeField] public Slider damageTakenBar { get; set; }
    [field: SerializeField] public TextMeshProUGUI currentHealthText { get; set; }
    [field: SerializeField] public Gradient healthBarColorGradient { get; set; }
    [field: SerializeField] public Image healthBarColor { get; set; }
    public List<AbilityEffectSlot> buffSlots;
    public List<AbilityEffectSlot> debuffSlots;
    public CoreCombat CoreCombat;


    private void Start()
    {
        healthBar.maxValue = health;
        damageTakenBar.maxValue = health;
        healthBar.value = health;
        damageTakenBar.value = health;
        healthBarColor.color = healthBarColorGradient.Evaluate(1f);
        currentHealthText.text = health.ToString("0") +"/" + healthBar.maxValue;
    }
    public void TakeDamage(float damage)
    {
        if (isInvincible || isDead)
        {
            return;
        }
        health -= damage;
        DamagePopup damagePopup = ProjectilePool.Instance.damagePopupPool.Get();
        damagePopup.SetDamagePopup(damage,transform.position);
        if (health<=0)
        {
            health = 0;
            isDead = true;
        }
        healthBar.value = health;
        DOTween.To(()=>damageTakenBar.value, x=>damageTakenBar.value=x, health, 1);
        healthBarColor.color = healthBarColorGradient.Evaluate(healthBar.normalizedValue);
        currentHealthText.text = health.ToString("0") + "/" + healthBar.maxValue;
    }
    public void GetKnockback()
    {

    }
    public void TakeDotDamage(float tickDamage,int tickCount,float tickRate )
    {
        StartCoroutine(TakeDotDamageCorotuine(tickDamage, tickCount, tickRate));
    }
    public IEnumerator TakeDotDamageCorotuine(float tickDamage, int tickCount, float tickRate)
    {
        yield return new WaitForSeconds(0.5f);
        while (tickCount > 0)
        {
            Debug.Log("hit");
            yield return new WaitForSeconds(tickRate);
            tickCount--;
            TakeDamage(tickDamage);
        }
    }
    public virtual void ApplyEffect(AbilityEffects effect,int buffType,AbilityEffectSO abilityEffectSO)
    {
        
    }
    
    public void ContinueEffect()
    {
        foreach (AbilityEffectSlot item in debuffSlots)
        {
            item.abilityEffectType.DuringEffect(CoreCombat);
        }
    }
    public IEnumerator ClearEffect(AbilityEffectSlot effect, int buffType)
    {
        yield return new WaitForSeconds(effect.abilityEffectType.effectDuration);
        if (buffType == 0)
        {
            effect.abilityEffectType.OnClear(CoreCombat);
            buffSlots.Remove(effect);
        }
        else if (buffType == 1)
        {
            effect.abilityEffectType.OnClear(CoreCombat);
            debuffSlots.Remove(effect);
        }
    }
    private void Update()
    {
        ContinueEffect();
        //foreach (AbilityEffectSlot debuff in debuffSlots)
        //{
        //    debuff.abilityEffectType.DuringEffect(ThirdPersonController.Instance);
        //}
    }   
}
