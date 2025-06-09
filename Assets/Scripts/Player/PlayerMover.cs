using UnityEngine;
using Zenject;

public class PlayerMover
{
    private Rigidbody rigidbody;
    private Transform transform;
    
    [Inject]
    private void Construct(PlayerComponents playerComponents)
    {
        rigidbody = playerComponents.Rigidbody;
        transform = playerComponents.Transform;
    }
    
    public void Move(Vector3 delta)
    {
        rigidbody.MovePosition(transform.position + delta);
    }
}