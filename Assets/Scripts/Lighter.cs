using System.Collections;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Lighter : MonoBehaviour
{
    [Range(50f, 10f)] [SerializeField] private float lerpSpeed = 25f;
    private Vector2 _mousePosition;

    [SerializeField] private float initialLife = 100f;

    private float _life;

    [SerializeField] private float lifeLossGap = 1f;

    private void Start()
    {
        _life = initialLife;
        Debug.Log(initialLife + " life is " + _life);
        StartCoroutine(ReduceLife());
    }
    
    void Update()
    {
        UpdateLighterMovement();
    }

    private void UpdateLighterMovement()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
            _life -= lifeLossGap;
            Debug.Log(_life);
            CheckLife();
            yield return new WaitForSeconds(1);
        }
    }
}