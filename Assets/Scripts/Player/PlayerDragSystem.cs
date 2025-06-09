using System;
using UnityEngine;
using Zenject;

public class PlayerDragSystem : IDragSource
{
    public bool Dragging => currentDragable != null;

    private IDragable currentDragable;

    [Serializable]
    public class Settings
    {
        public Transform DesirablePosition;
    }

    private Settings settings;
    
    [Inject]
    private void Construct(Settings settings)
    {
        this.settings = settings;
    }

    public void StartDrag(IDragable dragable)
    {
        currentDragable = dragable;
        currentDragable.StartDrag(this);
    }

    public void Update()
    {
        if(currentDragable == null) return;
        
        var newPosition = Vector3.MoveTowards(currentDragable.Position, GetDesiredPosition(), GetDragMoveDelta());
        currentDragable.Move(newPosition);
    }

    public void EndDrag()
    {
        currentDragable.OnDragEnd();
        currentDragable = null;
    }

    private float GetDragMoveDelta()
    {
        return Time.fixedDeltaTime * 2.5f;
    }
    
    private Vector3 GetDesiredPosition()
    {
        return settings.DesirablePosition.position;
    }
}
