using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Transform start, end;
    
    public Vector3 RawDirection => (end.position - start.position).normalized;

    public Vector2 Direction
    {
        get
        {
            var rawDirection = RawDirection;
            return new Vector2(rawDirection.x, rawDirection.z);
        }
    }

    public bool LookForward(out GameObject gameObject)
    {
        return Look(start.position, RawDirection, 1.5f, out gameObject);
    }
    
    private bool Look(Vector3 start, Vector3 direction, float distance, out GameObject result)
    {
        bool hasHit = Physics.Raycast(start, direction, out RaycastHit hit, distance, 255, QueryTriggerInteraction.Ignore);
        result = hasHit ? hit.collider.gameObject : null;
        return hasHit;
    }
}
