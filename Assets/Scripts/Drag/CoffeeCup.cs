using UnityEngine;

public class CoffeeCup : MonoBehaviour, IDragable
{
    [SerializeField] private GameObject coffeeFull;
    [SerializeField] private Rigidbody rigidbody;

    public bool Full { get; set; }
    public Rigidbody Rigidbody => rigidbody;
    
    public void Empty()
    {
        Full = false;
        coffeeFull.SetActive(false);
    }
    
    public void MakeFull()
    {
        Full = true;
        coffeeFull.SetActive(true);
    }
    
    public void Destroy()
    {
        EndDragManually();
        Destroy(gameObject);
    }
    
    #region IDragable implementation

    public bool CanStartDrag { get; set; } = true;
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
    #endregion
}
