using UnityEngine;

public class PlayerLookDirection : MonoBehaviour
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
}
