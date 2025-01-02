using Entities;
using UnityEngine;

public class HealthPickupController : MonoBehaviour
{
    [SerializeField] private float _healthBonus = 10f;
    
    public void GivePickupBonus(GameObject pickup, GameObject other)
    {
        var player = other.GetComponent<EntityHealthComponent>();
        player.UpdateHealth(_healthBonus);
        Destroy(this.gameObject);
    }
}
