using UnityEngine;

public class Lighter : MonoBehaviour
{
    [Range(50f, 10f)]
    [SerializeField] private float lerpSpeed = 25f;
    private Vector2 _mousePosition;
    
    // Update is called once per frame
    void Update()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Vector2.Lerp(transform.position, _mousePosition, lerpSpeed * Time.deltaTime);
    }
}
