using UnityEngine;
using Zenject;

public class PlayerLogicLoop : ITickable
{
    private PlayerMover playerMover;
    private PlayerLookDirection playerLookDirection;
    private PlayerInput playerInput;
    private PlayerSettings playerSettings;
    
    [Inject]
    private void Construct(PlayerMover playerMover, PlayerLookDirection playerLookDirection, PlayerInput playerInput, PlayerSettings playerSettings)
    {
        this.playerMover = playerMover;
        this.playerLookDirection = playerLookDirection;
        this.playerInput = playerInput;
        this.playerSettings = playerSettings;
    }
    
    public void Tick()
    {
        LogicLoop();
    }

    private void LogicLoop()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 movement = playerInput.Movement;
        Vector2 lookDirection = playerLookDirection.Direction;

        if (movement == Vector2.zero)
            return;

        float angle = -Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg + 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
        Vector3 moveDelta = rotation * new Vector3(movement.x, 0f, movement.y);

        playerMover.Move(moveDelta * playerSettings.MovementSpeed * Time.fixedDeltaTime);
    }
}