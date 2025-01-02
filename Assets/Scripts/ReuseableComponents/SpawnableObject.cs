
using System;
using UnityEngine;

public class SpawnableObject : MonoBehaviour
{
    public Action<SpawnableObject> OnDespawnEvent;

    private void OnDestroy()
    {
        OnDespawnEvent?.Invoke(this);
    }
}
