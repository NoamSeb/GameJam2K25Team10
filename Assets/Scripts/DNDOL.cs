using UnityEngine;

public class DNDOL : MonoBehaviour
{
    public static DNDOL Instance;

    public bool bKonami;

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
}
