using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class Lighter : MonoBehaviour
{
    public static Lighter Instance;
    
    [Range(50f, 10f)] [SerializeField] private float lerpSpeed = 25f;
    private Vector3 _mousePosition;

    [Space] [Header("Lighter attributes")] [SerializeField]
    private float initialLife = 100f;

    [SerializeField] private Canvas deathCanvas;

    [SerializeField] private float lifeLossGap = 1f;
    [SerializeField] private float gainGasQuantity;
    [SerializeField] private float damageGasQuantity;
    [SerializeField] private float lifeLossMultiplierProgress;
    [SerializeField] private Slider lifeBarUI;
    [SerializeField] private int burningScore;

    private float _life;
    private float _multiplier = 1f;

    private int _score = 0;
    public int Score => _score;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }

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
            deathCanvas.gameObject.SetActive(true);
            Cursor.visible = true;
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

    public void TakeDamage()
    {
        Debug.Log("Lost some life");
        _life -= damageGasQuantity;
    }

    public void AddScore()
    {
        _score += burningScore;
    }

    public void AddGas()
    {
        _life += gainGasQuantity;
        if (_life >= 100.0f)
        {
            _life = 100.0f;
        }
    }
    
}