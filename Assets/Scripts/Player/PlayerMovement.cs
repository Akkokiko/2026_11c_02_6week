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
    
    [Header("Horizontal Movement")]
    [SerializeField] private float m_moveSpeed;
    
    
    void Start()
    {
        m_horizontalInput = Player.InputActionAsset.FindAction("MoveHorizontal");
    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_PlayerRigidbody.linearVelocityX = m_horizontalInput.ReadValue<float>() * m_moveSpeed;
    }
}
