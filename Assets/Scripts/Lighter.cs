using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
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
    [SerializeField] private Animator animator;

    [Header("UI elements")] [SerializeField]
    private GameObject spriteLight2D;

    [SerializeField] private GameObject Light2D;
    [Space] private float _life;
    public float Life => _life;
    private float _multiplier = 1f;

    private int _score = 0;
    public int Score => _score;

    private int _axeScoreStreak;
    private int _jerrycanScoreStreak;
    [SerializeField] private int unlockAxeScoreStreak;
    [SerializeField] private int unlockJerrycanScoreStreak;
    [SerializeField] private GameObject AxeAbilityCanvas;
    [SerializeField] private GameObject JerrycanAbilityCanvas;
    public bool _isAxeScoreStreakAvailable = false;
    private bool _isJerrycanAbilityAvailable = false;

    private bool IsJerrycanChanceIncreased = false;

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
        Cursor.visible = false;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.GasSfx);

        if (DNDOL.Instance.bKonami)
        {
            Light2D.SetActive(true);
            spriteLight2D.GetComponent<Light2D>().enabled = true;
        }
    }

    void Update()
    {
        UpdateLighterMovement();
        ReduceLife();
        _multiplier += Time.deltaTime * lifeLossMultiplierProgress;
        if (Input.GetMouseButtonDown(0))
        {
            UseGasAbility();
        }

        if (Input.GetMouseButtonDown(1))
        {
            UseScoreStreak();
        }

        #region Abilities

        #region Axe

        if (_isAxeScoreStreakAvailable)
        {
            AxeAbilityCanvas.SetActive(true);
        }
        else
        {
            AxeAbilityCanvas.SetActive(false);
        }

        #endregion

        #region Jerrycan

        if (_isJerrycanAbilityAvailable)
        {
            JerrycanAbilityCanvas.SetActive(true);
        }
        else
        {
            JerrycanAbilityCanvas.SetActive(false);
        }

        #endregion

        #endregion
    }

    private void UpdateLighterMovement()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mousePosition.z = -50f;
        transform.position = Vector2.Lerp(transform.position, _mousePosition, lerpSpeed * Time.deltaTime);
    }

    private void CheckLife()
    {
        if (_life > 30f)
        {
            if (IsJerrycanChanceIncreased)
            {
                IsJerrycanChanceIncreased = false;
                JerricanGenerator.Instance.JerrycanSpawnChance += 20;
                Debug.Log(JerricanGenerator.Instance.JerrycanSpawnChance);
            }
        }

        if (_life <= 30f)
        {
            if (!IsJerrycanChanceIncreased)
            {
                IsJerrycanChanceIncreased = true;
                JerricanGenerator.Instance.JerrycanSpawnChance -= 20;
                Debug.Log(JerricanGenerator.Instance.JerrycanSpawnChance);
            }
        }

        if (_life <= 0f)
        {
            Debug.Log("Game Over !");
            deathCanvas.gameObject.SetActive(true);
            Cursor.visible = true;
            GameObject restartCanvas = GameObject.FindGameObjectWithTag("RestartCanvas");
            Animator animRestartCanvas = restartCanvas.GetComponent<Animator>();
            animRestartCanvas.SetTrigger("Death");
            Destroy(gameObject);
        }
    }

    private void ReduceLife()
    {
        _life -= lifeLossGap * _multiplier * Time.deltaTime;
        lifeBarUI.value = Mathf.Lerp(lifeBarUI.value, _life, 1f);
        CheckLife();
    }

    public void TakeDamage()
    {
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

    #region Special Ability

    public void AddSpecialGas()
    {
        _life += gainGasQuantity * 2;
        if (_life >= 100.0f)
        {
            _life = 100.0f;
        }
    }

    public void AddScoreStreak()
    {
        _axeScoreStreak++;
        _jerrycanScoreStreak++;
        
        if (_axeScoreStreak >= unlockAxeScoreStreak)
            _isAxeScoreStreakAvailable = true;

        if (_jerrycanScoreStreak >= unlockJerrycanScoreStreak)
            _isJerrycanAbilityAvailable = true;
    }

    public void ResetScoreStreak()
    {
        _axeScoreStreak = 0;
        _jerrycanScoreStreak = 0;
    }

    private void UseScoreStreak()
    {
        if (_isAxeScoreStreakAvailable)
        {
            AxeAnim.Instance.Slash();
            _isAxeScoreStreakAvailable = false;
            _axeScoreStreak = 0;

            GameObject[] badRopesToKill = GameObject.FindGameObjectsWithTag("RopeBad");
            GameObject[] goodRopesToKill = GameObject.FindGameObjectsWithTag("RopeGood");

            foreach (GameObject badRope in badRopesToKill)
            {
                Destroy(badRope);
            }

            foreach (GameObject goodRope in goodRopesToKill)
            {
                Destroy(goodRope);
            }
        }
    }

    private void UseGasAbility()
    {
        if (_isJerrycanAbilityAvailable)
        {
            _isAxeScoreStreakAvailable = false;
            AddSpecialGas();
            _isJerrycanAbilityAvailable = false;
            _jerrycanScoreStreak = 0;
        }
    }

    #endregion
}