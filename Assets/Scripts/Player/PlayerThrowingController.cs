using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerThrowingController : MonoBehaviour
{
    [SerializeField] private Player m_Player;
    public Player Player => m_Player;
    
    [SerializeField] private Transform m_AimIndicator;
    public Transform AimIndicator => m_AimIndicator;
    
    
    public InputAction m_fireInput;



    public void Update()
    {

        Vector3 rotation = aimDirection();
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;    
        m_AimIndicator.localRotation = Quaternion.Euler(0f, 0f, rotZ);
    }


    Vector3 aimDirection()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mouseWorldPosition.z =  transform.position.z;
        Vector3 rotation = mouseWorldPosition - transform.position;
        
        return rotation;
    }

    
}
