using Sound;
using UnityEngine;

public class CoinPickup : AutoPickup2D
{


    [SerializeField] private Sex m_coinSound;
    
    
    public override void pickup(Player player)
    {
        base.pickup(player);
        player.PlayerInventory.addCoin(1);
        m_coinSound.Play(transform.position);
        Destroy(gameObject);
        
    }
}
