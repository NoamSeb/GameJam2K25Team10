using System;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundSetter : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private SpriteRenderer backgroundSprite;
    [SerializeField] private Sprite baseBackgroundImage;
    [SerializeField] private Sprite konamiBackgroundImage;

    private void Reset()
    {
        backgroundSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        backgroundSprite.sprite = DNDOL.Instance.bKonami ? konamiBackgroundImage : baseBackgroundImage;
    }
}
