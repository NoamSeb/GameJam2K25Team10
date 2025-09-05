using System;
using UnityEngine;

public class AxeAnim : MonoBehaviour
{
    public static AxeAnim Instance;
    [SerializeField] private Animator animator;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
    }

    
    public void Slash()
    {
        animator.SetTrigger("Slash");
        AudioManager.Instance.PlaySFX(AudioManager.Instance.SlashSfx);
    }
}