using UnityEngine;
using Zenject;

public class PlayerLogicLoop : ITickable
{
    private PlayerMover playerMover;
    private PlayerLook playerLook;
    private PlayerInput playerInput;
    private PlayerSettings playerSettings;
    private PlayerDragSystem playerDragSystem;
    private PlayerHUD playerHUD;
    
    [Inject]
    private void Construct(PlayerMover playerMover, 
        PlayerLook playerLook,
        PlayerInput playerInput, 
        PlayerSettings playerSettings,
        PlayerDragSystem playerDragSystem,
        PlayerHUD playerHUD)
    {
        this.playerMover = playerMover;
        this.playerLook = playerLook;
        this.playerInput = playerInput;
        this.playerSettings = playerSettings;
        this.playerDragSystem = playerDragSystem;
        this.playerHUD = playerHUD;
    }
    
    public void Tick()
    {
        LogicLoop();
    }

    private void LogicLoop()
    {
        HandleMovement();
        HandleDrag();
    }

    private void HandleMovement()
    {
        Vector2 movement = playerInput.Movement;
        Vector2 lookDirection = playerLook.Direction;

        if (movement == Vector2.zero)
            return;

        float angle = -Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg + 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
        Vector3 moveDelta = rotation * new Vector3(movement.x, 0f, movement.y);

        playerMover.Move(moveDelta * (playerSettings.MovementSpeed * Time.fixedDeltaTime));
    }

    private void HandleDrag()
    {
        if (playerDragSystem.Dragging)
        {
            if (playerInput.LMBUp)
            {
                playerDragSystem.EndDrag();
            }
            else
            {
                playerDragSystem.Update();
            }
        }
        else
        {
            GameObject lookedObject = null;
            bool lookingAtObject = playerLook.LookForward(out lookedObject);
            IDragable dragable = null;
            bool lookingAtDragable = lookingAtObject && lookedObject.TryGetComponent(out dragable);
            
            playerHUD.SetInteractionDotVisibility(lookingAtDragable);
            
            if (playerInput.LMBDown && lookingAtObject)
            {
                if (lookingAtDragable)
                {
                    playerDragSystem.StartDrag(dragable);
                    playerHUD.SetInteractionDotVisibility(false);
                }
            }
        }
    }
}