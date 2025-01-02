using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1.5f;
    [SerializeField] private float _kickbackSize = 5f;
    [SerializeField] private float _playerDamage = 2f;

    [SerializeField] private GameObject[] _dropPrefabs;
    [Range(0,1)] [SerializeField] private float _dropOdds;
    
    private GameObject _targetObject;
    
    private void Awake()
    {
        _targetObject = FindObjectOfType<PlayerController>()?.gameObject;
    }

    private void FixedUpdate()
    {
        var movementVector = _targetObject.transform.position - transform.position;
        movementVector.Normalize(); 
        movementVector *= _moveSpeed * Time.deltaTime;

        transform.position += movementVector;
    }

    public float GetDamage() => _playerDamage;

    public void KickBack()
    {
        var kickbackVector = transform.position - _targetObject.transform.position;
        kickbackVector.Normalize(); 
        kickbackVector *= _kickbackSize;
        
        transform.position += kickbackVector;
    }

    public void HandlePickupDrop()
    {
        var odds = Random.Range(0f, 1f);
        if (odds >= _dropOdds)
        {
            return;
        }
        
        int index = Random.Range(0, _dropPrefabs.Length);
        var drop = _dropPrefabs[index];
        
        Instantiate(drop, transform.position, Quaternion.identity);
    }
}
