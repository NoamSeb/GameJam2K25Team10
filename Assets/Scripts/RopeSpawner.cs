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
    [SerializeField] private bool bHasRope = false;
    private bool bIsSpawningRope = false;

    [Header("Respawn Values")]
    [Range(0.1f, 10.0f)]
    [SerializeField] private float respawnTime = 1.0f;
    
    void Start()
    {
        
    }

    void Update()
    {
        RopeCheck();
    }

    private void RopeCheck()
    {
        // Si null, il n'y a pas de rope
        if (!currentRope && !bIsSpawningRope)
        {
            StartCoroutine(SpawnNewRope());
        }
    }

    private IEnumerator SpawnNewRope()
    {
        bIsSpawningRope = true;
        yield return new WaitForSeconds(respawnTime);

        int ropeTypeIndex = Random.Range(0, ropeTypes.Length);
        GameObject newRope = Instantiate(ropeTypes[ropeTypeIndex], transform.position, Quaternion.identity);
        currentRope = newRope;
        bIsSpawningRope = false;
        
        Debug.Log("Spawned: " + newRope.GetType().Name);
    }
}
