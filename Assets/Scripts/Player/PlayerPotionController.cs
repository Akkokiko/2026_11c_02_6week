using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerPotionController : MonoBehaviour
{
    [SerializeField] private Player m_Player;
    public Player Player => m_Player;
    
    [SerializeField] private Transform m_AimIndicator;
    public Transform AimIndicator => m_AimIndicator;
    
    private InputAction m_fireInput;
    private InputAction m_drinkInput;
    private InputAction m_potionSwapScroll;

    
    public int m_potionInHand = 0;
    
    
    [Header("Throwing")] 
    [SerializeField] private PotionProjectile m_potionProjectilePref;
    [SerializeField] private Transform m_throwingPoint;
    [SerializeField] private float m_throwingForce;

    void Awake()
    {
        m_fireInput = Player.InputActionAsset.FindAction("Fire");
        m_drinkInput = Player.InputActionAsset.FindAction("Drink");
        m_potionSwapScroll = Player.InputActionAsset.FindAction("Potion_Swap_Scroll");
        
        UIManager.instance.InventoryUI.chosePotion(m_potionInHand);
    }

    public void Update()
    {

        Vector3 rotation = aimDirection();
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;    
        m_AimIndicator.localRotation = Quaternion.Euler(0f, 0f, rotZ);

        if (m_fireInput.WasPressedThisFrame())
        {
            Throw();
        }

        if (m_drinkInput.WasPressedThisFrame())
        {
            Drink();
        }


        m_potionSwapScroll.performed += x => swapPotion_Scroll(x.ReadValue<float>());


    }


    void swapPotion_Scroll(float scrollValue)
    {
        m_potionInHand += (int)Mathf.Sign(scrollValue);

        if (m_potionInHand < 0) m_potionInHand = Player.PlayerInventory.getPotionInventorySize() - 1;
        if(m_potionInHand >= Player.PlayerInventory.getPotionInventorySize())  m_potionInHand = 0;
        
        UIManager.instance.InventoryUI.chosePotion(m_potionInHand);
    }
    
    Vector3 aimDirection()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mouseWorldPosition.z =  transform.position.z;
        Vector3 rotation = mouseWorldPosition - transform.position;
        
        return rotation;
    }

    void Throw()
    {
        if (!Player.PlayerInventory.isSlotEmpty(m_potionInHand))
        {
            Vector3 direction = aimDirection();
            PotionProjectile projectile = Instantiate(m_potionProjectilePref, m_throwingPoint.position,Quaternion.identity);
            projectile.Lauch(direction, Player.PlayerInventory.GetPotion(m_potionInHand), m_throwingForce);
            Player.PlayerInventory.removePotion(m_potionInHand);
        }
    }

    void Drink()
    {
        
        Potion potion = Player.PlayerInventory.GetPotion(m_potionInHand);
        potion.Affect(Player);
        Player.PlayerInventory.removePotion(m_potionInHand);
    }


}
