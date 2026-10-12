using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Player m_Player;
    [SerializeField] private Rigidbody2D m_PlayerRigidbody;
    public Player Player => m_Player;
    public Rigidbody2D PlayerRigidbody => m_PlayerRigidbody; 

    private InputAction m_horizontalInput;
    private InputAction m_jumpInput;

    private bool m_isGrounded;
    private bool m_jumpPressed;
    private LayerMask m_groundedMask;
    
    [Header("Horizontal Movement")]
    [SerializeField] private float m_moveSpeed;

    [Header("Jump")]
    [SerializeField] private float m_jumpHeight;
    
    
    void Start()
    {
        m_horizontalInput = Player.InputActionAsset.FindAction("MoveHorizontal");
        m_jumpInput = Player.InputActionAsset.FindAction("Jump");

        m_groundedMask = LayerMask.GetMask("Floor");
    
    }
    
    void Update()
    {
        m_isGrounded = m_PlayerRigidbody.IsTouchingLayers(m_groundedMask);

        m_jumpPressed = m_jumpInput.ReadValue<float>() == 1;
        
        
    }
    void FixedUpdate()
    {
        m_PlayerRigidbody.linearVelocityX = m_horizontalInput.ReadValue<float>() * m_moveSpeed;
        
        if (m_isGrounded && m_jumpPressed)
        {
            m_PlayerRigidbody.linearVelocityY =  m_jumpHeight;
        }
    }
}
