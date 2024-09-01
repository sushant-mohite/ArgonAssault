using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float timeToDestroy = 3f;
    void Start()
    {
        Destroy(gameObject, 3f);
    }
}
