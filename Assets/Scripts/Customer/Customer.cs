using System.Collections;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private Animator animator;

    [SerializeField] private Transform buyPoint;
    [SerializeField] private Transform awayPoint;
    
    private bool coffeeRecieved = false;
    
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        
        yield return WalkToPosition(buyPoint.position);
        yield return new WaitUntil(() => coffeeRecieved == true);
        yield return WalkToPosition(awayPoint.position);
    }

    private IEnumerator WalkToPosition(Vector3 position)
    {
        SetMovementAnimation();
        
        while (DistanceWithoutY(transform.position, position) > 0.1f)
        {
            var lookDirection = (position - transform.position).normalized;
            var newPos = Vector3.MoveTowards(transform.position, position, Time.deltaTime * 3);
            MoveToPosition(newPos);
            LookInDirection(lookDirection);
            yield return null;
        }
        
        SetIdleAnimation();
    }

    private float DistanceWithoutY(Vector3 from, Vector3 to)
    {
        from.y = 0;
        to.y = 0;
        return Vector3.Distance(from, to);
    }

    private void MoveToPosition(Vector3 position)
    {
        rigidbody.MovePosition(position);
    }

    private void LookInDirection(Vector3 lookDirection)
    {
        var angle = -Mathf.Atan2(lookDirection.z, lookDirection.x) * Mathf.Rad2Deg;
        var eulers = transform.eulerAngles;
        eulers.y = angle;
        transform.eulerAngles = eulers;
    }

    private void SetMovementAnimation()
    {
        animator.SetBool("Walking", true);
    }
    
    private void SetIdleAnimation()
    {
        animator.SetBool("Walking", false);
    }
    
    
}
