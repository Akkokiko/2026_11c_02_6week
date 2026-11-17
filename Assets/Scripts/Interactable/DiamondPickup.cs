using UnityEngine;
using Sound;
public class DiamondPickup : AutoPickup2D
{
    [SerializeField] private Sex diamondSe;
    public override void pickup(Player player)
    {

        base.pickup(player);
        player.PlayerInventory.addDiamond(1);
        diamondSe.Play(transform.position);
        
        Destroy(gameObject);
        
    }
}
