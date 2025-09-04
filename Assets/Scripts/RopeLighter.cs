using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RopeLighter : MonoBehaviour
{
    [SerializeField] private bool bIsBeingLit = false;
    [SerializeField] private bool bIsLit = false;
    [SerializeField] private float lightingTime = 1.0f;

    //[SerializeField] private bool bIsBadRope = false;

    void Update()
    {
        if (bIsLit)
        {
            return;
        }
        
        if (!bIsBeingLit)
        {
            return;
        }

        lightingTime -= Time.deltaTime;

        if (lightingTime <= 0.0f)
        {
            bIsLit = true;
            Debug.Log("Lit up");

            if (gameObject.CompareTag("RopeBad"))
            {
                Lighter.Instance.TakeDamage();
                Debug.Log("Removing Life");
            }
            else
            {
                Lighter.Instance.AddScore();
                Debug.Log("Score Increased !");

                if (GetComponent<JerricanGenerator>().bHasJerrycan)
                {
                    Lighter.Instance.AddGas();
                }
            }
            StartCoroutine(WaitFire());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Lighter"))
        {
            return;
        }

        Debug.Log("Entered");
        bIsBeingLit = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Lighter"))
        {
            return;
        }

        Debug.Log("Left");
        bIsBeingLit = false;
    }

    IEnumerator WaitFire()
    {
        GetComponentInChildren<Animator>().SetTrigger("IsBurning");
        yield return new WaitForSecondsRealtime(0.65f);
        Destroy(this.GameObject());
    }
}
