 using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
public class ThirdPersonController : MonoBehaviour
{
    public static ThirdPersonController Instance;
    [Header("Player")]
    public CameraFollow playerCamera;
    private Animator playerAnimator;
    private CharacterController playerCharacterController;
    public Collider playerCollider;
    private Rigidbody playerRigidbody;
    private StarterAssetsInputs playerInput;
    [Header("PlayerMovement")]
    [SerializeField] private float currentMoveSpeed;
    [SerializeField] private float defaultMoveSpeed = 5.335f;
    [SerializeField] private float speedChangeRate;
    [SerializeField] private float changedMoveSpeed;
    [SerializeField] private float actionMoveSpeed;
    [SerializeField] private float apliedMoveSpeed;
    [SerializeField] private MovementState movementState;
    private Vector3 inputDirection;
    private float animationBlend;
    [field:SerializeField] public bool isRooted {  get; set; }
    [field:SerializeField] public bool isSilenced {  get; set; }

    [Header("Player Grounded")]
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private float groundedOffset = -0.14f;
    [SerializeField] private float groundedRadius = 0.28f;
    [SerializeField] private LayerMask SolidLayers;

    [Header("Player Abillitys")]
    public Transform abilitySpawnPoint;
    public List<Ability> playerAbilitys;
    public Ability currentlyCastingAbility;
    public List<Slider> playerAbilitysUI;
    public GameObject aoeAbilityIndicator;
    public GameObject abilityCastRangeIndicator;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerCharacterController = GetComponent<CharacterController>();
        playerInput = GetComponent<StarterAssetsInputs>();
        playerRigidbody = GetComponent<Rigidbody>();
        for (int i = 0; i < playerAbilitys.Count; i++)
        {
            playerAbilitys[i].SetUIElements(playerAbilitysUI[i]);
            playerAbilitysUI[i].gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        //Move();
        GroundedCheck();
    }
    private void GroundedCheck()
    {
        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset,
            transform.position.z);
        isGrounded = Physics.CheckSphere(spherePosition, groundedRadius, SolidLayers,
            QueryTriggerInteraction.Ignore);
    }
    private void Move()
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
        switch (movementState)
        {
            case MovementState.Normal:
                playerCharacterController.Move(new Vector3(inputDirection.x, 0, inputDirection.z) * defaultMoveSpeed * Time.deltaTime);
                break;
            case MovementState.Action:
                if (actionMoveSpeed<changedMoveSpeed)
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
    public void ChangeMoveSpeedOnAction(float percentage, float changeSpeed)
    {
        float newSpeed = defaultMoveSpeed;
        actionMoveSpeed = defaultMoveSpeed;
        newSpeed += (percentage / 100f) * defaultMoveSpeed;
        movementState = MovementState.Action;
        DOTween.To(() => actionMoveSpeed, x => actionMoveSpeed = x, newSpeed, changeSpeed).OnComplete(() => movementState = MovementState.Changed);
    }
    public float ChangeMoveSpeedOvertime(float percentage, float changeSpeed)
    {
        float currentSpeed = changedMoveSpeed;
        changedMoveSpeed += percentage / 100f * changedMoveSpeed;
        speedChangeRate=Mathf.Abs(apliedMoveSpeed - changedMoveSpeed)/changeSpeed;
        movementState = MovementState.Changed;
        return Mathf.Abs(currentSpeed - changedMoveSpeed);
    }
    public float ChangeMoveSpeedInstant(float percentage)
    {
        float currentSpeed = changedMoveSpeed;
        changedMoveSpeed += (percentage / 100f) * changedMoveSpeed;
        apliedMoveSpeed = changedMoveSpeed;
        movementState = MovementState.Changed;
        return Mathf.Abs(currentSpeed - changedMoveSpeed);
    }
    public void RegainMoveSpeedOvertime(float amount, float changeSpeed)
    {
        changedMoveSpeed += amount;
        speedChangeRate=Mathf.Abs(apliedMoveSpeed - changedMoveSpeed)/changeSpeed;
    }
    public void RegainMoveSpeedInstant(float amount)
    {
        changedMoveSpeed += amount;
    }
    public void GetStunned(bool _isStunned)
    {
        isRooted = _isStunned;
        movementState = MovementState.Rooted;
        isSilenced = _isStunned;
    }
    public void GetSilenced(bool _isSilenced)
    {
        isSilenced = _isSilenced;
    }
    public void GetRooted(bool _isRooted)
    {
        movementState = MovementState.Rooted;
        isRooted = _isRooted;
    }
    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (isGrounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z),groundedRadius);
    }
}