using Sound;
using UnityEngine;

public class CoinPickup : AutoPickup2D
{
    [SerializeField] private Sex m_coinSound;

    private string m_coinID;
    public string CoinID => m_coinID;

    private void Awake()
    {   
        string parentName = transform.parent.name;
        // string bagName = parentName.Replace("CoinsLvl", "");

        string objectName = gameObject.name;
        string itemName = objectName == "Coin"
        ? "0"
        : objectName.Replace("Coin (", "").Replace(")", "");

        m_coinID = parentName + "__" + itemName;
    }

    private void Start()
    {
        if (SaveController.Instance.IsCoinCollected(m_coinID))
        {
            Destroy(gameObject);
        }
    }

    public override void pickup(Player player)
    {
        base.pickup(player);

        player.PlayerInventory.addCoin(1);
        m_coinSound.Play(transform.position);
        SaveController.Instance.RegisterCoinCollected(m_coinID);

        Destroy(gameObject);
    }
}
