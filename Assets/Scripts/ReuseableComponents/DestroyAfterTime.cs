using System.Collections;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] private float _secondsToDestroy = 1f;

    private void Awake()
    {
        StartCoroutine(DestroyAfterSeconds());
    }

    private IEnumerator DestroyAfterSeconds()
    {
        yield return new WaitForSeconds(_secondsToDestroy);
        Destroy(gameObject);
    }
}
