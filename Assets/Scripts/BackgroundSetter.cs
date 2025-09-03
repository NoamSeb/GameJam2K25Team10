using System;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundSetter : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Sprite baseBackgroundImage;
    [SerializeField] private Sprite konamiBackgroundImage;
    
    private void Start()
    {
        backgroundImage.sprite = DNDOL.Instance.bKonami ? konamiBackgroundImage : baseBackgroundImage;
    }
}
