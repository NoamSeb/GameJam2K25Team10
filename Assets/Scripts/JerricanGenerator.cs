using UnityEngine;

public class JerricanGenerator : MonoBehaviour
{
    public bool bHasJerrycan;
    [Range(0, 100)] [SerializeField] private int jerrycanSpawnChance;
    
    [Header("Ropes Sprites")]
    [SerializeField] private Sprite[] jerrycanSprites;
    
    void Start()
    {
        int ropeTypeIndex = Random.Range(0, 100);
        bHasJerrycan = ropeTypeIndex >= jerrycanSpawnChance;

        if (!bHasJerrycan)
        {
            return;
        }

        GetComponent<SpriteRenderer>().sprite = jerrycanSprites[Random.Range(0, jerrycanSprites.Length)];
    }
}
