using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class JerricanGenerator : MonoBehaviour
{
    public static JerricanGenerator Instance;
    
    public bool bHasJerrycan;
    [Range(0, 100)] [SerializeField] private int jerrycanSpawnChance;

    public int JerrycanSpawnChance
    {
        get => jerrycanSpawnChance;
        set => jerrycanSpawnChance = value;
    }

    [Header("Ropes Sprites")]
    [SerializeField] private Sprite[] jerrycanSprites;

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
        bHasJerrycan = ropeTypeIndex >= jerrycanSpawnChance;

        if (!bHasJerrycan)
        {
            return;
        }

        GetComponent<SpriteRenderer>().sprite = jerrycanSprites[Random.Range(0, jerrycanSprites.Length)];
    }
}
