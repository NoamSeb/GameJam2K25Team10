using UnityEngine;

public class BombGenerator : MonoBehaviour
{
    public static BombGenerator Instance;
    
    public bool bHasBomb;
    [Range(0, 100)] [SerializeField] private int bombSpawnChance;

    public int JerrycanSpawnChance
    {
        get => bombSpawnChance;
        set => bombSpawnChance = value;
    }

    [Header("Bomb Sprites")]
    [SerializeField] private Sprite[] bombSprites;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        int ropeTypeIndex = Random.Range(0, 100);
        bHasBomb = ropeTypeIndex >= bombSpawnChance;

        if (!bHasBomb)
        {
            return;
        }

        GetComponent<SpriteRenderer>().sprite = bombSprites[Random.Range(0, bombSprites.Length)];
    }
}
