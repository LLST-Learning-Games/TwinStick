using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entities
{
    public class PlayerAttackComponent : EntityComponent
    {
        [SerializeField] private GameObject _targetPrefab;
        [SerializeField] private GameObject _aimer;
        [SerializeField] private List<ProjectileController> _projectilePrefabs;
        [SerializeField] private Animator _upperBodyAnimator;
        
        [SerializeField] private bool _fixedTargetDistance = true;
        [SerializeField] private float _targetDistance = 1f;
        
        [SerializeField] private float _maxAmmo = 100f;
        [SerializeField] private float _startingAmmo = 100f;

        private int _currentProjectileIndex = 0;
        
        private float _timeSinceProjectile = 0f;
        private GameObject _target;
        private PlayerAmmoUiView _playerAmmoUi;
        private PlayerController _playerController;

        public override void Initialize(GameObject parent)
        {
            _playerController = parent?.GetComponent<PlayerController>();
            _playerAmmoUi = FindObjectOfType<PlayerAmmoUiView>();
            _playerAmmoUi.UpdateAmmoValueText(_startingAmmo);
            _target = Instantiate(_targetPrefab);
        }

        public override void UpdateComponent()
        {
            HandleTarget();
            
            if (_playerController.IsDead)
            {
                return;
            }
            
            HandleProjectile();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                UpdateAmmo(10f);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _currentProjectileIndex = 0;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _currentProjectileIndex = 1;
            }
        }

        private void HandleProjectile()
        {
            if (Input.GetMouseButton(0))
            {
                if(_startingAmmo > 0 && _timeSinceProjectile <= 0.0f)
                {
                    ProjectileController projectile = Instantiate(_projectilePrefabs[_currentProjectileIndex]);
                    projectile.IntializeProjectile(_aimer.transform);
                    _timeSinceProjectile = projectile.GetDelay();
                    _startingAmmo--;
                    _playerAmmoUi.UpdateAmmoValueText(_startingAmmo);
                }
                _timeSinceProjectile -= Time.fixedDeltaTime;
            } 
        }
        
        private void HandleTarget()
        {
            var mousPos = Input.mousePosition;
            var worldPos = Camera.main.ScreenToWorldPoint(mousPos);
            
            if(_fixedTargetDistance)
            {
                var directionVector = new Vector3(worldPos.x, worldPos.y, 0) - transform.position;
                directionVector.Normalize();
                
                if (_upperBodyAnimator)
                {
                    _upperBodyAnimator.SetFloat("X", directionVector.x);
                    _upperBodyAnimator.SetFloat("Y", directionVector.y);
                }

                Debug.Log($"[{GetType().Name}] MouseMos: {worldPos} Direction: {directionVector}]");
                _target.transform.position = transform.position + (directionVector * _targetDistance);
            }
            else
            {
                _target.transform.position = new Vector3(worldPos.x, worldPos.y, 0);
                
                var directionVector = new Vector3(worldPos.x, worldPos.y, 0) - transform.position;
                directionVector.Normalize();
                
                Debug.Log(directionVector);
                
                if (_upperBodyAnimator)
                {
                    _upperBodyAnimator.SetFloat("X", directionVector.x);
                    _upperBodyAnimator.SetFloat("Y", directionVector.y);
                }
            }
            
            _aimer.transform.right = _target.transform.position - transform.position;
        }
        
        public void UpdateAmmo(float deltaAmmo)
        {
            _startingAmmo += deltaAmmo;
            _playerAmmoUi.UpdateAmmoValueText(_startingAmmo);
        }
    }
}