using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;
[Serializable]
public class AbilityEffectDataHolder
{
    [field:SerializeField]public AbilityEffects abilityEffect { get; private set; }
    [field: SerializeField] public AbilityEffectSO AbilityEffectSO { get; private set; }
    [field: SerializeField] public AbilityEffectTypes AbilityEffectType { get; private set; }
    [field: SerializeField] public int effectType { get; private set; }
    //ctor if needed for later use
    public AbilityEffectDataHolder(AbilityEffects _abilityEffects,AbilityEffectSO _abilityEffectSO,AbilityEffectTypes _abilityEffectTypes,int _effectType)
    {
        abilityEffect = _abilityEffects;
        AbilityEffectSO = _abilityEffectSO;
        AbilityEffectType = _abilityEffectTypes;
        effectType = _effectType;
    }
    public void SetAbilityEffect(AbilityEffects _abilityEffects)
    {
        abilityEffect = _abilityEffects;
    }
}
public abstract class Ability : MonoBehaviour
{
    public PlayerAbilitySO abilitySO;
    public List<AbilityEffectDataHolder> abilityEffectData;
    public virtual void Trigger() { }
    public virtual void Cast() { }
    public virtual void Casting() { }
    public virtual void OnCooldown() { }
    public virtual void CanceledChannel() { }
    public virtual void Cancel() { }
    public virtual void Channeling() { }
    public virtual void Effect(CoreHealth enemy) { }
    public virtual void TriggerEndAnim() { }
    [SerializeField] float _cooldownTime;
    [SerializeField] float _castTime;
    [SerializeField] float _useTime;
    public AbilityState abilityState;
    Slider abilitySlot;
    TextMeshProUGUI cooldownText;
    public ThirdPersonController caster { get; private set; }
    public CoreCombat combat;
    private void Start()
    {
        caster = ThirdPersonController.Instance;
        foreach (AbilityEffectDataHolder ability in abilityEffectData)
        {
            switch (ability.AbilityEffectType)
            {
                case AbilityEffectTypes.Stun:
                    if (ability.AbilityEffectSO is StunEffectSO) ability.SetAbilityEffect(new Stun((StunEffectSO)ability.AbilityEffectSO));
                    break;
                case AbilityEffectTypes.Incapacitate:
                    break;
                case AbilityEffectTypes.Petrify:
                    break;
                case AbilityEffectTypes.Panic:
                    break;
                case AbilityEffectTypes.Slow:
                    if (ability.AbilityEffectSO is SlowEffectSO) ability.SetAbilityEffect(new Slow((SlowEffectSO)ability.AbilityEffectSO));
                    break;
                case AbilityEffectTypes.FadingSlow:
                    if (ability.AbilityEffectSO is FadingSlowEffectSO) ability.SetAbilityEffect(new FadingSlow((FadingSlowEffectSO)ability.AbilityEffectSO));
                    break;
                case AbilityEffectTypes.Silence:
                    if (ability.AbilityEffectSO is SilenceEffectSO) ability.SetAbilityEffect(new Silence((SilenceEffectSO)ability.AbilityEffectSO));
                    break;
                case AbilityEffectTypes.Weaken:
                    break;
                case AbilityEffectTypes.Blind:
                    break;
                case AbilityEffectTypes.Broken:
                    break;
                case AbilityEffectTypes.DamageOverTime:
                    if (ability.AbilityEffectSO is DamageOverTimeEffectSO) ability.SetAbilityEffect(new DamagePerSecond((DamageOverTimeEffectSO)ability.AbilityEffectSO));
                    break;
                case AbilityEffectTypes.Root:
                    if (ability.AbilityEffectSO is RootEffectSO) ability.SetAbilityEffect(new Root((RootEffectSO)ability.AbilityEffectSO));
                    break;
                case AbilityEffectTypes.None:
                    break;
                default:
                    break;
            }
        }
    }
    void Update()
    {
        switch (abilityState)
        {
            case AbilityState.SetReady:
                abilitySlot.transform.DOMoveY(90, 0.1f).SetEase(Ease.OutSine);
                _castTime = abilitySO.castTime;
                _useTime = abilitySO.useTime;
                _cooldownTime = abilitySO.cooldownTime;
                abilitySlot.maxValue = abilitySO.cooldownTime;
                cooldownText.text = "";
                ChangeAbilityState(AbilityState.Ready);
                break;
            case AbilityState.Ready:
                break;
            case AbilityState.Triggered:
                Trigger();
                //abilitySO.slowAmountWhileCasting=caster.ChangeMoveSpeedOvertime(-abilitySO.slowPercentageWhileCasting, abilitySO.castTime);
                combat.CoreMovement.ChangeMoveSpeedOnAction(-abilitySO.slowPercentageWhileCasting, abilitySO.castTime);
                abilitySlot.fillRect.transform.gameObject.SetActive(true);
                abilitySlot.transform.DOMoveY(abilitySlot.transform.position.y + 10, 0.1f).SetEase(Ease.OutSine);
                ChangeAbilityState(AbilityState.Casting);
                break;
            case AbilityState.Casting:
                Casting();
                _castTime -= Time.deltaTime;
                if (_castTime <= 0)
                {
                    abilitySlot.value = abilitySO.cooldownTime;
                    abilitySlot.transform.DOMoveY(abilitySlot.transform.position.y - 25, 0.1f).SetEase(Ease.OutSine);
                    ChangeAbilityState(AbilityState.Casted);
                }
                break;
            case AbilityState.Canceled:
                Cancel();
                //caster.RegainMoveSpeedOvertime(+abilitySO.slowAmountWhileCasting, 0.1f);
                caster.currentlyCastingAbility = null;
                abilitySlot.transform.DOMoveY(abilitySlot.transform.position.y - 10, 0.1f).SetEase(Ease.OutSine);
                abilitySlot.maxValue = abilitySO.cooldownTimeAfterCancel;
                _cooldownTime = abilitySO.cooldownTimeAfterCancel;
                _useTime = 0;
                ChangeAbilityState(AbilityState.OnCooldown);
                break;
            case AbilityState.Casted:
                Cast();
                ChangeAbilityState(AbilityState.Channeling);
                break;
            case AbilityState.CanceledChanneling:
                CanceledChannel();
                caster.currentlyCastingAbility = null;
                //caster.RegainMoveSpeedOvertime(+abilitySO.slowAmountWhileCasting, 0.1f);
                ChangeAbilityState(AbilityState.OnCooldown);
                break;
            case AbilityState.Channeling:
                Channeling();
                _useTime -= Time.deltaTime;
                if (_useTime<=0)
                {
                    //caster.RegainMoveSpeedOvertime(+abilitySO.slowAmountWhileCasting, 0.1f);
                    caster.currentlyCastingAbility = null;
                    ChangeAbilityState(AbilityState.OnCooldown);

                }
                break;
            case AbilityState.OnCooldown:
                OnCooldown();
                //if (_useTime > 0)
                //{
                //    _useTime -= Time.deltaTime;
                //    Cast();
                //}
                //if (abilitySO.cooldownWhileActive)
                //{
                //    _cooldownTime -= Time.deltaTime;
                //}
                //else
                //{
                //    //cooldown when skill ended
                //    if (_useTime <= 0)
                //        _cooldownTime -= Time.deltaTime;
                //}
                //actual cooldown with uý
                _cooldownTime -= Time.deltaTime;
                abilitySlot.value = _cooldownTime;
                cooldownText.text = _cooldownTime.ToString("0.0");
                if (_cooldownTime <= 0)
                {
                    ChangeAbilityState(AbilityState.SetReady);
                }
                break;
            default:
                break;
        }
    }
    public void ChangeAbilityState(AbilityState state)
    {
        abilityState = state;
    }
    public void SetUIElements(Slider _slider)
    {
        abilitySlot = _slider;
        cooldownText = abilitySlot.GetComponentInChildren<TextMeshProUGUI>();
        abilitySlot.transform.GetChild(1).GetComponent<Image>().sprite = abilitySO.icon;
    }
}
