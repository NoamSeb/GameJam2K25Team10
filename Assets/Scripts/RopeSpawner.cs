using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class RopeSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject ropeGood;
    [SerializeField] private GameObject ropeBad;
    [SerializeField] private GameObject currentRope;
    
    [SerializeField] private GameObject[] ropeTypes;
    
    [Header("Check Values")]
    [SerializeField] private bool bIsSpawningRope = false;

    [Header("Respawn Values")]
    [Range(0.1f, 10.0f)]
    [SerializeField] private float respawnTime = 1.0f;

    [Header("Chance Values")]
    [SerializeField] private int goodSpawnChance = 100;
    
    [Header("Ropes Sprites")]
    [SerializeField] private Sprite[] goodRopesSprite;
    [SerializeField] private Sprite[] badRopesSprite;

    void Update()
    {
        RopeCheck();
    }

    
    private void RopeCheck()
    {
        if (!currentRope && !bIsSpawningRope)
        {
            StartCoroutine(SpawnNewRope());
        }
    }

    private IEnumerator SpawnNewRope()
    {
        bIsSpawningRope = true;
        yield return new WaitForSeconds(respawnTime);

        GameObject ropeChoice;

        int ropeTypeIndex = Random.Range(0, 100);
        ropeChoice = ropeTypeIndex >= goodSpawnChance ? ropeTypes[0] : ropeTypes[1];
        
        GameObject newRope = Instantiate(ropeChoice, transform.position, Quaternion.identity);

        if (newRope.GetComponent<RopeGood>())
        {
            newRope.gameObject.tag = "RopeGood";
            newRope.GetComponent<SpriteRenderer>().sprite = goodRopesSprite[Random.Range(0, goodRopesSprite.Length)];
        }
        else if (newRope.GetComponent<RopeBad>())
        {
            newRope.gameObject.tag = "RopeBad";
            newRope.GetComponent<SpriteRenderer>().sprite = badRopesSprite[Random.Range(0, badRopesSprite.Length)];
        }
        
        currentRope = newRope;
        bIsSpawningRope = false;
        
        Debug.Log("Spawned: " + newRope.GetType().Name);
    }
}
