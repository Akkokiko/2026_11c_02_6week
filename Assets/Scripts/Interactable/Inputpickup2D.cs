using LitMotion;
using UnityEngine;
using UnityEngine.UI;

public class InteractPickup2D : Interactable
{
    [SerializeField] private Flower flower;
    [SerializeField] private int amount;
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    
    [Header("Respawn")]
    [SerializeField] bool canRespawn = true;

    [SerializeField] private float respawnTime = 10;
    [SerializeField] private Image m_cooldownIndicator;
    

    private void Start()
    {
        m_spriteRenderer.sprite = flower.sprite;
        m_cooldownIndicator.enabled = false;
    }

    public override void Interact(Player player)
    {
        Debug.Log("Added " + flower.name + " to " + player.name);
        player.PlayerInventory.AddFlower(flower, amount);
        if (canRespawn)
        {
            m_spriteRenderer.sprite = null;
            m_cooldownIndicator.enabled = true;
            m_intereactionCollider.enabled = false; 
            LMotion.Create(0f, 1f, respawnTime).WithOnComplete(respawn).Bind(x => m_cooldownIndicator.fillAmount = x);
            removeFromInteractaion();
        }
        else
        {
            removeFromInteractaion();
            Destroy(gameObject);
        }

       
    }

    void respawn()
    {
        m_cooldownIndicator.enabled = false;
        m_spriteRenderer.sprite = flower.sprite;
        m_intereactionCollider.enabled = true;
    }


}