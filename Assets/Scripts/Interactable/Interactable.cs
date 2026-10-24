using UnityEngine;

public class Interactable : MonoBehaviour
{
    private Player playerInRange;
    private bool playerNearby = false;

    [SerializeField] protected Collider2D m_intereactionCollider;

    private  void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<Player>(out var player))
            {
                player.PlayerInteractController.addNearbyInteractable(this);
                playerInRange = player;
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
                removeFromInteractaion();
            }
            playerNearby = false;
        }
    }

    public virtual void Interact(Player player)
    {
        
    }

    protected virtual void removeFromInteractaion()
    {
        if(!playerNearby)
            return;
        playerInRange.PlayerInteractController.removeNearbyInteractable(this);
        playerInRange = null;
    }
}
