using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum MovementState
{
    Normal,
    Action,
    Changed,
    Rooted,
}
public class PlayerMovement : CoreMovement
{
    [SerializeField] private MovementState playerMovementState;
    private Animator playerAnimator;
    private CharacterController playerCharacterController;
    public Collider playerCollider;
    private StarterAssetsInputs playerInput;
    private Vector3 inputDirection;
    private float animationBlend;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerCharacterController = GetComponent<CharacterController>();
        playerInput = GetComponent<StarterAssetsInputs>();
    }
    public override void Update()
    {
        base.Update();
    }
    public override void Move()
    {
        apliedMoveSpeed = Mathf.MoveTowards(apliedMoveSpeed, changedMoveSpeed, speedChangeRate * Time.deltaTime);
        currentMoveSpeed = playerCharacterController.velocity.magnitude;
        animationBlend = Mathf.Lerp(animationBlend, apliedMoveSpeed, Time.deltaTime * speedChangeRate);
        if (animationBlend < 0.01f) animationBlend = 0f;
        // normalise input direction
        inputDirection = new Vector3(playerInput.move.x, 0.0f, playerInput.move.y);
        Vector3 movement = new Vector3(playerInput.move.x, 0.0f, playerInput.move.y);
        movement *= playerCharacterController.velocity.magnitude * Time.deltaTime;
        float velocityZ = Vector3.Dot(movement.normalized, transform.forward);
        float velocityX = Vector3.Dot(movement.normalized, transform.right);
        playerAnimator.SetFloat("VelocityZ", velocityZ, 0.1f, Time.deltaTime);
        playerAnimator.SetFloat("VelocityX", velocityX, 0.1f, Time.deltaTime);
        if (playerInput.move == Vector2.zero)
        {
            playerCharacterController.Move(Vector3.zero);
            return;
        }
        switch (playerMovementState)
        {
            case MovementState.Normal:
                playerCharacterController.Move(new Vector3(inputDirection.x, 0, inputDirection.z) * defaultMoveSpeed * Time.deltaTime);
                break;
            case MovementState.Action:
                if (actionMoveSpeed < changedMoveSpeed)
                {
                    playerCharacterController.Move(new Vector3(inputDirection.x, 0, inputDirection.z) * actionMoveSpeed * Time.deltaTime);
                }
                else
                {
                    playerCharacterController.Move(new Vector3(inputDirection.x, 0, inputDirection.z) * apliedMoveSpeed * Time.deltaTime);
                }
                break;
            case MovementState.Changed:
                playerCharacterController.Move(new Vector3(inputDirection.x, 0, inputDirection.z) * apliedMoveSpeed * Time.deltaTime);
                break;
            case MovementState.Rooted:
                playerCharacterController.Move(Vector3.zero);
                break;
            default:
                break;
        }
    }
    public override float ChangeMoveSpeedOvertime(float percentage, float changeSpeed)
    {
        playerMovementState = MovementState.Changed;
        return base.ChangeMoveSpeedOvertime(percentage, changeSpeed);
    }
    public override float ChangeMoveSpeedInstant(float percentage)
    {
        playerMovementState = MovementState.Changed;
        return base.ChangeMoveSpeedInstant(percentage);
    }
    public override void ChangeMoveSpeedOnAction(float percentage, float changeSpeed)
    {
        base.ChangeMoveSpeedOnAction(percentage, changeSpeed);
        float newSpeed = defaultMoveSpeed;
        actionMoveSpeed = defaultMoveSpeed;
        newSpeed += (percentage / 100f) * defaultMoveSpeed;
        playerMovementState = MovementState.Action;
        DOTween.To(() => actionMoveSpeed, x => actionMoveSpeed = x, newSpeed, changeSpeed).OnComplete(() => playerMovementState = MovementState.Changed);
    }
    public override void RegainMoveSpeedInstant(float amount)
    {
        base.RegainMoveSpeedInstant(amount);
    }
    public override void RegainMoveSpeedOvertime(float amount, float changeSpeed)
    {
        base.RegainMoveSpeedOvertime(amount, changeSpeed);
    }
    public override void GetStunned(bool _isStunned)
    {
        base.GetStunned(_isStunned);
        playerMovementState = MovementState.Rooted;
    }
    public override void GetRooted(bool _isRooted)
    {
        base.GetRooted(_isRooted);
        playerMovementState = MovementState.Rooted;
    }
    public override void GetSilenced(bool _isSilenced)
    {
        base.GetSilenced(_isSilenced);
    }
}
