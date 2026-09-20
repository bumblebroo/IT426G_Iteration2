using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    [SerializeField]
    private float delay;

    void Start()
    {
        Destroy(this.gameObject, delay);
    }
}
