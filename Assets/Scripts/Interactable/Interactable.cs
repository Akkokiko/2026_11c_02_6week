using UnityEngine;

public class Interactable : MonoBehaviour
{
    private Player playerInRange;
    private bool playerNearby = false;
    
    [SerializeField] protected Collider2D m_intereactionCollider;
    
    [SerializeField ] private SpriteRenderer m_intereactionSpriteRenderer;
    bool m_hasHighlightOption = false;

    protected virtual void Start()
    {
        m_hasHighlightOption = m_intereactionSpriteRenderer != null && m_intereactionSpriteRenderer.material != null &&
            m_intereactionSpriteRenderer.material.HasProperty("_isHovered");

    }


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
        
        unHover();
        playerInRange.PlayerInteractController.removeNearbyInteractable(this);
        playerInRange = null;
    }

    public void Hover()
    {
        if (m_hasHighlightOption)
        {
            m_intereactionSpriteRenderer.material.SetFloat("_isHovered", 1);
        }
    }

    public void unHover()
    {
        if (m_hasHighlightOption)
        {
            m_intereactionSpriteRenderer.material.SetFloat("_isHovered", 0);
        }
    }
}
