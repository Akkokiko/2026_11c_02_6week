using UnityEngine;

public class DiamondPickup : AutoPickup2D
{
    public override void pickup(Player player)
    {
        base.pickup(player);
        player.PlayerInventory.addDiamond(1);
        
        Destroy(gameObject);
        
    }
}
