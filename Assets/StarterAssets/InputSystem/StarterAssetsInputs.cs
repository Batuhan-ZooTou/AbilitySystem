using UnityEngine;
using UnityEngine.InputSystem;
public class StarterAssetsInputs : MonoBehaviour
{
	private ThirdPersonController playerController;
	[Header("Character Input Values")]
	public Vector2 move;
	public Vector2 look;
	public bool jump;
	public bool sprint;
	public bool onAction = false;
	public bool delayAfterCancel = false;
	public bool holdinShift = false;
	public bool actionCancel = false;
	public bool canMove = true;
	public float mouseScrollY;
	public bool lockCam;
	[Header("Movement Settings")]
	public bool analogMovement;
	private bool switchLock = false;

	[Header("Mouse Cursor Settings")]
	public bool cursorLocked = true;
	public bool cursorInputForLook = true;

    private void Start()
    {
		playerController = ThirdPersonController.Instance;
	}
    public void OnMove(InputAction.CallbackContext context)
	{
		move = context.ReadValue<Vector2>();
	}
	public void OnLook(InputAction.CallbackContext context)
	{
		look = context.ReadValue<Vector2>();
	}
	public void OnJump(InputAction.CallbackContext context)
	{
		jump = context.ReadValueAsButton();
	}
	public void OnSprint(InputAction.CallbackContext context)
	{
		sprint = context.ReadValueAsButton();
	}
	public void OnScrollWheel(InputAction.CallbackContext context)
	{
		mouseScrollY = -context.ReadValue<Vector2>().y;
		playerController.playerCamera.ZoomCam(mouseScrollY);
	}
	public void OnActionCancel(InputAction.CallbackContext context)
    {
        if (context.started)
        {
			for (int i = 0; i < playerController.playerAbilitys.Count; i++)
			{
				if (playerController.playerAbilitys[i].abilityState == AbilityState.Casting)
				{
					playerController.playerAbilitys[i].ChangeAbilityState(AbilityState.Canceled);
					return;
				}
				else if (playerController.playerAbilitys[i].abilityState == AbilityState.Channeling)
				{
					playerController.playerAbilitys[i].ChangeAbilityState(AbilityState.CanceledChanneling);
					return;
				}
			}
		}
    }
	public void OnLMB(InputAction.CallbackContext context)
	{
        
		if (context.started)
		{
            if (playerController.currentlyCastingAbility!=null)
            {
				return;
			}
			//Check if its cooldowned
			if (playerController.playerAbilitys[0].abilityState==AbilityState.Ready)
            {
				//Cast the ability
				playerController.playerAbilitys[0].ChangeAbilityState(AbilityState.Triggered);
				playerController.currentlyCastingAbility = playerController.playerAbilitys[0];
			}
		}
	}
	public void OnRMB(InputAction.CallbackContext context)
	{
        if (playerController.isSilenced)
        {
			return;
        }
		if (context.started)
		{
			if (playerController.currentlyCastingAbility != null)
			{
				return;
			}
			//Check if its cooldowned
			if (playerController.playerAbilitys[1].abilityState == AbilityState.Ready)
			{
				//Cast the ability
				playerController.playerAbilitys[1].ChangeAbilityState(AbilityState.Triggered);
				playerController.currentlyCastingAbility = playerController.playerAbilitys[1];

			}
		}
	}
	public void OnPressQ(InputAction.CallbackContext context)
	{
		if (playerController.isSilenced)
		{
			return;
		}
		if (context.started)
		{
			if (playerController.currentlyCastingAbility != null)
			{
				if (playerController.currentlyCastingAbility == playerController.playerAbilitys[2])
				{
					return;
				}
				playerController.currentlyCastingAbility.ChangeAbilityState(AbilityState.Canceled);
			}
			//Check if its cooldowned
			if (playerController.playerAbilitys[2].abilityState == AbilityState.Ready)
			{
				//Cast the ability
				playerController.playerAbilitys[2].ChangeAbilityState(AbilityState.Triggered);
				playerController.currentlyCastingAbility = playerController.playerAbilitys[0];
			}
		}
	}
	public void OnPressE(InputAction.CallbackContext context)
    {
		if (context.started)
		{
			//Check if its cooldowned
			if (playerController.playerAbilitys[2].abilityState == AbilityState.Ready)
			{
				//Cast the ability
				playerController.playerAbilitys[2].ChangeAbilityState(AbilityState.Triggered);

			}
		}
	}
	public void OnPressR(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			//Check if its cooldowned
			if (playerController.playerAbilitys[3].abilityState == AbilityState.Ready)
			{
				//Cast the ability
				playerController.playerAbilitys[3].ChangeAbilityState(AbilityState.Triggered);

			}
		}
	}
	public void OnPressF(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			//Check if its cooldowned
			if (playerController.playerAbilitys[4].abilityState == AbilityState.Ready)
			{
				//Cast the ability
				playerController.playerAbilitys[4].ChangeAbilityState(AbilityState.Triggered);

			}
		}
	}
	public void OnPressSpace(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			//Check if its cooldowned
			if (playerController.playerAbilitys[4].abilityState == AbilityState.Ready)
			{
				//Cast the ability
				playerController.playerAbilitys[4].ChangeAbilityState(AbilityState.Triggered);

			}
		}
	}
	public void OnMMB(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			playerController.playerCamera.camLocked = !playerController.playerCamera.camLocked;
		}
	}
}