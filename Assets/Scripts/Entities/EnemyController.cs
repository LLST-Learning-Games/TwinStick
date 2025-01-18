using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1.5f;
    [SerializeField] private float _kickbackSize = 5f;
    [SerializeField] private float _playerDamage = 2f;

    [SerializeField] private GameObject[] _dropPrefabs;
    [Range(0,1)] [SerializeField] private float _dropOdds;

    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private float _deathFadeoutTime = 2f;
    
    public bool IsDead { get; private set; }
    private GameObject _targetObject;
    
    
    private void Awake()
    {
        _targetObject = FindObjectOfType<PlayerController>()?.gameObject;
    }

    private void FixedUpdate()
    {
        if (IsDead)
        {
            return;
        }
        
        var movementVector = _targetObject.transform.position - transform.position;
        movementVector.Normalize(); 
        movementVector *= _moveSpeed * Time.deltaTime;

        transform.position += movementVector;
    }

    public float GetDamage()
    {
        return IsDead ? _playerDamage : 0f;
    }

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

    public void OnDie()
    {
        if (IsDead)
        {
            return;
        }
        IsDead = true;
        HandlePickupDrop();
        StartCoroutine(OnDieCoroutine());
    }

    private IEnumerator OnDieCoroutine()
    {
        float time = 0f;
        while (time < _deathFadeoutTime)
        {
            time += Time.deltaTime;
            _sprite.material.SetFloat("_DissolveAmount", time / _deathFadeoutTime);
            yield return null;
        }
        Destroy(this.gameObject);
    }
    
    
}
