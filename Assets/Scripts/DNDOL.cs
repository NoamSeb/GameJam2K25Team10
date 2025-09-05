using System;
using UnityEngine;

public class DNDOL : MonoBehaviour
{
    public static DNDOL Instance;

    public bool bKonami;
    public int restartedGames = 0;

    private void Awake()
    {
        bKonami = false;
        
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            bKonami = !bKonami;
        }
    }
}
