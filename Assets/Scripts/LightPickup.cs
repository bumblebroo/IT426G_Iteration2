using System.Collections;
using UnityEngine;

public class LightPickup : MonoBehaviour
{
    [SerializeField]
    private float speed;

    public void SetTarget(Transform target) {
        StartCoroutine(MoveTowardsTarget(target));
    }

    private IEnumerator MoveTowardsTarget(Transform target) {
        while (true) {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
}
