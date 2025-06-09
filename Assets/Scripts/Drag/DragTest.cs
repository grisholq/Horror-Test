using UnityEngine;

public class DragTest : MonoBehaviour, IDragable
{
    [SerializeField] private Rigidbody rigidbody;
    
    public Vector3 Position => transform.position;
    
    private IDragSource source;
    
    public void Move(Vector3 position)
    {
        rigidbody.MovePosition(position);
    }

    public void StartDrag(IDragSource source)
    {
        this.source = source;
        rigidbody.useGravity = false;
    }

    public void EndDragManually()
    {
        source?.EndDrag();
    }

    public void OnDragEnd()
    {
        rigidbody.useGravity = true;
    }
}
