using Entities;
using UnityEngine;

public class AmmoPickupController : MonoBehaviour
{
    [SerializeField] private float _ammoBonus = 10f;
    
    public void GivePickupBonus(GameObject pickup, GameObject other)
    {
        var player = other.GetComponent<PlayerAttackComponent>();
        player.UpdateAmmo(_ammoBonus);
        Destroy(this.gameObject);
    }
}
