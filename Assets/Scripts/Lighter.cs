using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class Lighter : MonoBehaviour
{
    [Range(50f, 10f)] [SerializeField] private float lerpSpeed = 25f;
    private Vector3 _mousePosition;

    [Space] [Header("Lighter attributes")] [SerializeField]
    private float initialLife = 100f;

    [SerializeField] private float lifeLossGap = 1f;
    [SerializeField] private float gainGasQuantity;
    [SerializeField] private float lifeLossMultiplierProgress;
    [SerializeField] private Slider lifeBarUI;

    private float _life;
    private float _multiplier = 1f;

    private void Start()
    {
        _life = initialLife;
        StartCoroutine(ReduceLife());
        Cursor.visible = false;
    }

    void Update()
    {
        UpdateLighterMovement();
        _multiplier += Time.deltaTime*lifeLossMultiplierProgress;
    }

    private void UpdateLighterMovement()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mousePosition.z = -50f;
        transform.position = Vector2.Lerp(transform.position, _mousePosition, lerpSpeed * Time.deltaTime);
    }

    private void CheckLife()
    {
        if (_life <= 0f)
        {
            Debug.Log("Game Over !");
            Destroy(gameObject);
        }
    }

    IEnumerator ReduceLife()
    {
        while (true)
        {
            _life -= lifeLossGap * _multiplier;
            lifeBarUI.value = Mathf.Lerp(lifeBarUI.value, _life, 1f);
            CheckLife();
            yield return new WaitForSecondsRealtime(1);
        }
    }
    
}