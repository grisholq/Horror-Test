using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screamer : MonoBehaviour
{
    [SerializeField] private Light light1;
    [SerializeField] private Light light2;
    [SerializeField] private GameObject bonny;
    [SerializeField] private AudioSource scarySound;
    [SerializeField] private AudioSource bonnyHello;
    [SerializeField] private Customer customer;
    [SerializeField] private Camera camera;
    [SerializeField] private Transform lookPoint;
    
    private IEnumerator Start()
    {
         yield return new WaitUntil(() => customer.Finished);
        yield return new WaitForSeconds(15);
        bonnyHello.Play();
        yield return new WaitUntil(CameraLooksAtScreamer);
        SetLightActivity(false);
        yield return new WaitForSeconds(0.5f);
        SetLightActivity(true);
        yield return new WaitForSeconds(0.25f);
        SetLightActivity(false);
        yield return new WaitForSeconds(0.15f);
        SetLightActivity(true);  
        yield return new WaitForSeconds(0.1f);
        SetLightActivity(false);      
        yield return new WaitForSeconds(0.1f);
        SetLightActivity(true);
        bonny.gameObject.SetActive(true);
        scarySound.Play();
        yield return new WaitForSeconds(3);
        SetLightActivity(false);
        bonny.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        SetLightActivity(true);
    }

    private void SetLightActivity(bool activity)
    {
        light1.enabled = activity;
        light2.enabled = activity;
    }

    private bool CameraLooksAtScreamer()
    {
        Vector3 screenPoint = camera.WorldToViewportPoint(lookPoint.transform.position);
        bool onScreen = screenPoint.x > 0.2f && 
                        screenPoint.x < 0.8 && 
                        screenPoint.y > 0.2f && screenPoint.y < 0.8;
        
        return onScreen;
    }
}