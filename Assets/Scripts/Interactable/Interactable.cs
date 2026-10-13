using UnityEngine;

public class Interactable : MonoBehaviour
{
    private bool playerNearby = false;

    private  void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<Player>(out var player))
            {
                player.PlayerInteractController.addNearbyInteractable(this);
            }

            playerNearby = true;
            

            Debug.Log("Press 'E' to pick up");
        }
    }

    private  void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<Player>(out var player))
            {
                player.PlayerInteractController.removeNearbyInteractable(this);
            }
            playerNearby = false;
        }
    }

    public virtual void Interact(Player player)
    {
        
    }
}
