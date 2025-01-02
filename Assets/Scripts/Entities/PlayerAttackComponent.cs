using UnityEngine;

namespace Entities
{
    public class PlayerAttackComponent : EntityComponent
    {
        [SerializeField] private GameObject _targetPrefab;
        [SerializeField] private GameObject _aimer;
        [SerializeField] private GameObject _projectilePrefab;
        
        [SerializeField] private float _maxAmmo = 100f;
        [SerializeField] private float _startingAmmo = 100f;
    
        [SerializeField] private float _projectileSpeed = 2f;
        [SerializeField] private float _projectileOffset = 1.5f;
        [SerializeField] private float _projectileDelay = 0.1f;
        
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
        
        private void HandleProjectile()
        {
            if (Input.GetMouseButton(0))
            {
                if(_startingAmmo > 0 && _timeSinceProjectile <= 0.0f)
                {
                    var projectile = Instantiate(_projectilePrefab);
                    projectile.transform.rotation = _aimer.transform.rotation;
                    projectile.transform.position =
                        _aimer.transform.position + _projectileOffset * projectile.transform.right;
                    var rb = projectile.GetComponent<Rigidbody2D>();
                    rb.velocity = projectile.transform.right * _projectileSpeed;
                    _timeSinceProjectile = _projectileDelay;
                    _startingAmmo--;
                    _playerAmmoUi.UpdateAmmoValueText(_startingAmmo);
                }
                _timeSinceProjectile -= Time.fixedDeltaTime;
            } 
        
            if (Input.GetKeyDown(KeyCode.P))
            {
                UpdateAmmo(10f);
            }
        }
        
        private void HandleTarget()
        {
            var mousPos = Input.mousePosition;
            var worldPos = Camera.main.ScreenToWorldPoint(mousPos);
        
            _target.transform.position = new Vector3(worldPos.x, worldPos.y, 0);
        
            _aimer.transform.right = _target.transform.position - transform.position;
        }
        
        public void UpdateAmmo(float deltaAmmo)
        {
            _startingAmmo += deltaAmmo;
            _playerAmmoUi.UpdateAmmoValueText(_startingAmmo);
        }
    }
}