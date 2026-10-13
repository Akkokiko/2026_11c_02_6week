using UnityEngine;

public class InteractPickup2D : Interactable
{
    [SerializeField] private Flower flower;
    [SerializeField] private int amount;

    public override void Interact(Player player)
    {
        Debug.Log("Added " + flower.name + " to " + player.name);
        player.PlayerInventory.AddFlower(flower, amount);
        Destroy(gameObject);
    }
}