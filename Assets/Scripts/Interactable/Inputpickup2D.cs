using UnityEngine;

public class InteractPickup2D : Interactable
{
    [SerializeField] private Flower flower;

    public override void Interact(Player player)
    {
        Debug.Log("Added " + flower.name + " to " + player.name);
        Destroy(gameObject);
    }
}