using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class RopeBase : MonoBehaviour
{
    [SerializeField] private bool bHasBeenLit;
    [SerializeField] private bool bIsBeingLit;
    [SerializeField] private float life;
    [SerializeField] private float timeBeforeGoingUp;

    [FormerlySerializedAs("fallMultiplier")] [Range(0, 10)] [SerializeField] private int fallTime;
    [Range(-100, 0)] [SerializeField] private float fallDistance;
    
    public bool BHasBeenLit
    {
        get => bHasBeenLit;
        set => bHasBeenLit = value;
    }

    public bool BIsBeingLit
    {
        get => bIsBeingLit;
        set => bIsBeingLit = value;
    }
    
    public float Life
    {
        get => life;
        set => life = value;
    }

    private void Start()
    {
        StartCoroutine(Fall());
    }

    private IEnumerator Fall()
    {
        float timesSinceStart = 0.0f;
        Vector2 targetPosition = new Vector2(transform.position.x, transform.position.y + fallDistance);

        while (timesSinceStart <= fallTime)
        {
            yield return null;
            
            transform.position = Vector2.Lerp(transform.position, targetPosition, timesSinceStart / fallTime);
            timesSinceStart += Time.deltaTime;
        }
        yield return new WaitForSecondsRealtime(timeBeforeGoingUp);
        StartCoroutine(GoingUp());
    }

    private IEnumerator GoingUp()
    {
        float timesSinceStart = 0.0f;
        Vector2 targetPosition = new Vector2(transform.position.x, transform.position.y - (fallDistance-10));

        while (timesSinceStart <= fallTime)
        {
            yield return null;
            
            transform.position = Vector2.Lerp(transform.position, targetPosition, timesSinceStart / fallTime);
            timesSinceStart += Time.deltaTime;
        }
        Destroy(gameObject);
    }
}
