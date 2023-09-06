using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMovement : CoreMovement
{
    public override void Update()
    {
        apliedMoveSpeed = Mathf.MoveTowards(apliedMoveSpeed, changedMoveSpeed, speedChangeRate * Time.deltaTime);
        base.Update();
    }
    public override float ChangeMoveSpeedOvertime(float percentage, float changeSpeed)
    {
        return base.ChangeMoveSpeedOvertime(percentage, changeSpeed);
    }
    public override float ChangeMoveSpeedInstant(float percentage)
    {
        return base.ChangeMoveSpeedInstant(percentage);
    }
    public override void ChangeMoveSpeedOnAction(float percentage, float changeSpeed)
    {
        base.ChangeMoveSpeedOnAction(percentage, changeSpeed);
    }
    public override void RegainMoveSpeedInstant(float amount)
    {
        base.RegainMoveSpeedInstant(amount);
    }
    public override void RegainMoveSpeedOvertime(float amount, float changeSpeed)
    {
        base.RegainMoveSpeedOvertime(amount, changeSpeed);
    }
    public override void GetRooted(bool _isRooted)
    {
        base.GetRooted(_isRooted);
    }
    public override void GetStunned(bool _isStunned)
    {
        base.GetStunned(_isStunned);
    }
    public override void GetSilenced(bool _isSilenced)
    {
        base.GetSilenced(_isSilenced);
    }
}
