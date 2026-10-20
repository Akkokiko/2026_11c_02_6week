using UnityEngine;

public class CoinPickup : AutoPickup2D{
    
    
    public override void pickup(Player player)
    {
        base.pickup(player);
        player.PlayerInventory.addCoin(1);
        
        Destroy(gameObject);
        
    }
}
