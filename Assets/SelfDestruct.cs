using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] private float timeToSelfDestruct = 3f;

    void Start()
    {
        // Destrói este GameObject após o tempo definido
        Destroy(gameObject, timeToSelfDestruct);
    }
}