using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerThrowingController : MonoBehaviour
{
    [SerializeField] private Player m_Player;
    public Player Player => m_Player;
    
    [SerializeField] private Transform m_AimIndicator;
    public Transform AimIndicator => m_AimIndicator;
    
    private InputAction m_fireInput;

    
    [Header("Throwing")]
    [SerializeField] private Transform m_throwingPoint;
    [SerializeField] private float m_throwingForce;

    void Awake()
    {
        m_fireInput = Player.InputActionAsset.FindAction("Fire");
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
        Vector3 direction = aimDirection();
        
        PotionProjectile projectile = Instantiate(Player.PotionProjectile, m_throwingPoint.position,Quaternion.identity);
        projectile.Lauch(direction,m_throwingForce);
    }


}
