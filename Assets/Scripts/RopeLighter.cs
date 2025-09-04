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

            if (gameObject.CompareTag("RopeBad"))
            {
                Lighter.Instance.TakeDamage();
                Lighter.Instance.ResetScoreStreak();
            }
            else
            {
                Lighter.Instance.AddScore();
                Lighter.Instance.AddScoreStreak();

                if (GetComponent<JerricanGenerator>().bHasJerrycan)
                {
                    Lighter.Instance.AddGas();
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.JerryCanSfx);
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
        
        bIsBeingLit = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Lighter"))
        {
            return;
        }
        
        bIsBeingLit = false;
    }

    IEnumerator WaitFire()
    {
        GetComponentInChildren<Animator>().SetTrigger("IsBurning");
        AudioManager.Instance.PlaySFX(AudioManager.Instance.FireSfx);
        yield return new WaitForSecondsRealtime(0.65f);
        Destroy(this.GameObject());
    }
}
