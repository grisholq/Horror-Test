using System;
using System.Collections;
using UnityEngine;

public class CoffeeMachine : MonoBehaviour
{
    [SerializeField] private GameObject coffeeFlow;
    [SerializeField] private Transform coffeeCupPosition;
    
    private CoffeeCup coffeeCup;
    private bool cooking = false;
    
    private IEnumerator CoffeeCookProcess()
    {
        coffeeCup.EndDragManually();
        coffeeCup.transform.position = coffeeCupPosition.position;
        coffeeCup.transform.eulerAngles = Vector3.zero;
        coffeeCup.Rigidbody.isKinematic = true;
        cooking = true;
        coffeeCup.CanStartDrag = false;
        coffeeFlow.SetActive(true);
        yield return new WaitForSeconds(4f);
        coffeeFlow.SetActive(false);
        coffeeCup.CanStartDrag = true;
        coffeeCup.MakeFull();
        cooking = false;
        coffeeCup.Rigidbody.isKinematic = false;
        coffeeCup = null;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (cooking) return;

        if (other.TryGetComponent(out CoffeeCup coffeeCup) == false || coffeeCup.Full) return;

        this.coffeeCup = coffeeCup;
        StartCoroutine(CoffeeCookProcess());
    }
}