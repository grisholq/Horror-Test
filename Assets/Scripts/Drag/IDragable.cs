using UnityEngine;

public interface IDragable
{
    public bool CanStartDrag { get; set; }
    public Vector3 Position { get; }
    public void Move(Vector3 position);
    public void StartDrag(IDragSource source);
    public void EndDragManually();
    public void OnDragEnd();
}