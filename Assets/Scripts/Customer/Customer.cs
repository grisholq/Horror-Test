using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Customer : MonoBehaviour
{
    [SerializeField] private float movespeed = 3;
    [SerializeField] private float rotateSpeed = 25;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource helloSound;
    [SerializeField] private AudioSource thanksSound;

    [SerializeField] private Transform enterPoint;
    [SerializeField] private Transform buyPoint;
    [SerializeField] private Transform awayPoint1;
    [SerializeField] private Transform awayPoint2;
    
    [SerializeField] private MultiAimConstraint headIK;

    public bool Finished => coffeeRecieved;
    
    private bool coffeeRecieved = false;
    
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);
        
        yield return WalkToPosition(enterPoint.position);
        yield return WalkToPosition(buyPoint.position);
        helloSound.Play();
        LookAtPlayer();
        yield return new WaitUntil(() => coffeeRecieved == true);
        thanksSound.Play();
        StopLookingAtPlayer();
        yield return WalkToPosition(awayPoint1.position);
        yield return WalkToPosition(awayPoint2.position);
    }

    private IEnumerator WalkToPosition(Vector3 position)
    {
        SetMovementAnimation();
        
        while (DistanceWithoutY(transform.position, position) > 0.1f)
        {
            var lookDirection = (position - transform.position).normalized;
            var newPos = Vector3.MoveTowards(transform.position, position, Time.deltaTime * movespeed);
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
        eulers.y = Mathf.MoveTowardsAngle(eulers.y, angle, Time.deltaTime * rotateSpeed);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<CoffeeCup>(out var coffee))
        {
            coffee.Destroy();
            coffeeRecieved = true;
        }
    }

    public void LookAtPlayer()
    {
        headIK.weight = 1;
    }

    public void StopLookingAtPlayer()
    {
        headIK.weight = 0;
    }
}
