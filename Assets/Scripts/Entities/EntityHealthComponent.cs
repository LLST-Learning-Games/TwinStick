using UnityEngine;

namespace Entities
{
    public class EntityHealthComponent : EntityComponent
    {    
        [SerializeField] private float _maxHealth = 100f;
        [SerializeField] private float _startingHealth = 100f;
        
        private PlayerHealthUiView _playerHealthUi;
        
        private PlayerController _playerController;

        public override void Initialize(GameObject parent)
        {
            _playerController = parent?.GetComponent<PlayerController>();
            _playerHealthUi = FindObjectOfType<PlayerHealthUiView>();
            _playerHealthUi.UpdateHealthValueText(_startingHealth);
        }

        public override void UpdateComponent()
        {
            //..
        }
        
        public void UpdateHealth(GameObject player, GameObject enemyObject)
        {
            var enemy = enemyObject.GetComponent<EnemyController>();
            _startingHealth -= enemy.GetDamage();

            UpdateHealthUi();
        }

        public void UpdateHealth(float deltaHealth)
        {
            _startingHealth += deltaHealth;
            UpdateHealthUi();
        }
    
        private void UpdateHealthUi()
        {
            if (_startingHealth <= 0)
            {
                _startingHealth = 0;
                _playerController.SetIsDead();
            }

            _playerHealthUi.UpdateHealthValueText(_startingHealth);
        }
    }
}