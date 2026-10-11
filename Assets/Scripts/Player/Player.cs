using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private BoxCollider2D m_PlayerCollider;
    public BoxCollider2D PlayerCollider => m_PlayerCollider;
    
    [SerializeField] private Rigidbody2D m_PlayerRigidbody;
    public Rigidbody2D PlayerRigidbody => m_PlayerRigidbody;
    
    
    
    [SerializeField] private Camera m_Camera;
    public Camera Camera => m_Camera;
    
    
    [SerializeField] private PotionProjectile m_PotionProjectile;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
