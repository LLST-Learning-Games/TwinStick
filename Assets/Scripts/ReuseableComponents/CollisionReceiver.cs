using UnityEngine;
using UnityEngine.Events;

public class CollisionReceiver : MonoBehaviour
{
    [SerializeField] private LayerMask _destroyLayerMask;
    [SerializeField] private UnityEvent<GameObject,GameObject> _onCollisionEvent;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Trigger with {other.gameObject.name}");
        OnCollisionEventCallback(other.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log($"Collision with {other.gameObject.name}");
        OnCollisionEventCallback(other.gameObject);
    }
    
    private void OnCollisionEventCallback(GameObject other)
    {
        int layerIndex = (int)Mathf.Log(_destroyLayerMask.value, 2);
        if (layerIndex == other.layer)
        {
            _onCollisionEvent?.Invoke(gameObject, other);
        }
    }

    public void DestroyThisObject()
    {
        Destroy(this.gameObject);
    }
}
