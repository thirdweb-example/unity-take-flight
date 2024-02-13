using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject, 20f);
    }
}
