using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    
    [Header("Player")]
    [SerializeField] private BoxCollider2D m_PlayerCollider;
    public BoxCollider2D PlayerCollider => m_PlayerCollider;
    
    [SerializeField] private Rigidbody2D m_PlayerRigidbody;
    public Rigidbody2D PlayerRigidbody => m_PlayerRigidbody;
    
    
    
    [SerializeField] private Camera m_Camera;
    public Camera Camera => m_Camera;

    [SerializeField] private InputActionAsset m_inputAction;
    public InputActionAsset InputActionAsset => m_inputAction;

    [Header("Player Components")]
    [SerializeField] private PlayerInventory m_playerInventory;
    public PlayerInventory PlayerInventory => m_playerInventory;
    
    [SerializeField] private PlayerMovement m_playerMovement;
    public PlayerMovement PlayerMovement => m_playerMovement;
    
    [SerializeField] private PlayerPotionController m_playerPotionController;
    public PlayerPotionController PlayerPotionController => m_playerPotionController;
    
    [SerializeField] private PlayerInteractController m_playerInteractController;
    public PlayerInteractController PlayerInteractController => m_playerInteractController;
    
    [SerializeField] private bool m_isInMenu;
    public bool IsInMenu => m_isInMenu;

    void OnEnable()
    {
        m_inputAction.Enable();
        m_isInMenu = false;
    }

    void OnDisable()
    {
        m_inputAction.Disable();
        m_isInMenu = true; 
    }

    public override void applyEffect(Effect effect)
    {
        base.applyEffect(effect);
        UIManager.instance.EffectBarUI.addEffectCard(effect);
    }

    public void swapIsInMenuState()
    {
        m_isInMenu = !m_isInMenu;
    }
}
