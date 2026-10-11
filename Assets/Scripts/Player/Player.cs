using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    [SerializeField] private BoxCollider2D m_PlayerCollider;
    public BoxCollider2D PlayerCollider => m_PlayerCollider;
    
    [SerializeField] private Rigidbody2D m_PlayerRigidbody;
    public Rigidbody2D PlayerRigidbody => m_PlayerRigidbody;
    
    
    
    [SerializeField] private Camera m_Camera;
    public Camera Camera => m_Camera;
    
    
    [SerializeField] private PotionProjectile m_PotionProjectile;
    public PotionProjectile PotionProjectile => m_PotionProjectile;




    [SerializeField] private InputActionAsset m_inputAction;
    public InputActionAsset InputActionAsset => m_inputAction;

    [Header("Player Components")]
    [SerializeField] private PlayerInventory m_playerInventory;
    public PlayerInventory PlayerInventory => m_playerInventory;

    void OnEnable()
    {
        m_inputAction.Enable();
    }

    void OnDisable()
    {
        m_inputAction.Disable();    
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
