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
        cooking = true;
        coffeeCup.CanStartDrag = false;
        coffeeFlow.SetActive(true);
        yield return new WaitForSeconds(4f);
        coffeeFlow.SetActive(false);
        coffeeCup.CanStartDrag = true;
        cooking = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (cooking) return;
        
        if (other.TryGetComponent(out CoffeeCup coffeeCup))
        {
            this.coffeeCup = coffeeCup;
            StartCoroutine(CoffeeCookProcess());
        }
    }
}