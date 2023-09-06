using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public abstract class CoreMovement : MonoBehaviour
{

    [Header("Movement")]
    public float currentMoveSpeed;
    [field: SerializeField] public float defaultMoveSpeed { get; set; }
    [field: SerializeField] public float speedChangeRate { get; set; }
    [field: SerializeField] public float changedMoveSpeed { get; set; }
    [field: SerializeField] public float actionMoveSpeed { get; set; }
    [field: SerializeField] public float apliedMoveSpeed { get; set; }
    
    [field: SerializeField] public bool isSilenced { get; set; }
    private void Start()
    {
        
    }

    public virtual void Update()
    {
        Move();
    }
    public virtual void Move()
    {
        
    }
    public virtual void ChangeMoveSpeedOnAction(float percentage, float changeSpeed)
    {
        
    }
    public virtual float ChangeMoveSpeedOvertime(float percentage, float changeSpeed)
    {
        float currentSpeed = changedMoveSpeed;
        changedMoveSpeed += percentage / 100f * changedMoveSpeed;
        speedChangeRate = Mathf.Abs(apliedMoveSpeed - changedMoveSpeed) / changeSpeed;
        return Mathf.Abs(currentSpeed - changedMoveSpeed);
    }
    public virtual float ChangeMoveSpeedInstant(float percentage)
    {
        float currentSpeed = changedMoveSpeed;
        changedMoveSpeed += (percentage / 100f) * changedMoveSpeed;
        apliedMoveSpeed = changedMoveSpeed;
        return Mathf.Abs(currentSpeed - changedMoveSpeed);
    }
    public virtual void RegainMoveSpeedOvertime(float amount, float changeSpeed)
    {
        changedMoveSpeed += amount;
        speedChangeRate = Mathf.Abs(apliedMoveSpeed - changedMoveSpeed) / changeSpeed;
    }
    public virtual void RegainMoveSpeedInstant(float amount)
    {
        changedMoveSpeed += amount;
        speedChangeRate = amount;
    }
    public virtual void GetStunned(bool _isStunned)
    {
        isSilenced = _isStunned;
    }
    public virtual void GetRooted(bool _isRooted)
    {
    }
    public virtual void GetSilenced(bool _isSilenced)
    {
        isSilenced = _isSilenced;
    }
}
