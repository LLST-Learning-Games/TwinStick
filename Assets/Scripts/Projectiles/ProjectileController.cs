using System;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    
    [SerializeField] private float _projectileSpeed = 3f;
    [SerializeField] private float _projectileDelay = 0.3f;
    [SerializeField] private float _projectileOffset = 0.25f;
    
    [SerializeField] private GameObject _collisionPrefab;
    
    [SerializeField] private Rigidbody2D _rb;
    
    public float GetDelay() => _projectileDelay;

    public void IntializeProjectile(Transform target)
    {
        transform.rotation = target.rotation;
        transform.position = target.position + _projectileOffset * transform.right;
        _rb.velocity = transform.right * _projectileSpeed;
    }

    private void OnDestroy()
    {
        if (_collisionPrefab)
        {
            var collision = Instantiate(_collisionPrefab, transform.position, Quaternion.identity);
        }
    }
}
