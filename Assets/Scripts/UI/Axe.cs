using System;
using UnityEngine;

public class Axe : MonoBehaviour
{
    [SerializeField] private GameObject axeImage;

    private void Start()
    {
        axeImage.SetActive(false);
    }

    private void Update()
    {
        if (Lighter.Instance._isAxeScoreStreakAvailable)
        {
            axeImage.SetActive(true);
        }
    }
}
