using Sound;
using UnityEngine;

public class CoinPickup : AutoPickup2D
{


    [SerializeField] private Sex m_coinSound;

    //[SerializeField] private string m_coinID;
    //public string CoinID => m_coinID;
    
    
    public override void pickup(Player player)
    {
        base.pickup(player);
        player.PlayerInventory.addCoin(1);
        m_coinSound.Play(transform.position);
        //SaveController.Instance.RegisterCoinCollected(m_coinID,player.PlayerInventory.m_coinAmount);
        Destroy(gameObject);
        
    }
}
