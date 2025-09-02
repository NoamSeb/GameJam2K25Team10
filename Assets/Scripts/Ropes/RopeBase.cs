using UnityEngine;

public class RopeBase : MonoBehaviour
{
    [SerializeField] private bool bHasBeenLit;
    [SerializeField] private bool bIsBeingLit;
    [SerializeField] private float life;
    
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
}
