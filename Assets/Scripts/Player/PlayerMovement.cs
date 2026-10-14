using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Player m_Player;
    private Rigidbody2D m_PlayerRigidbody;
    private Collider2D m_PlayerCollider;
    public Player Player => m_Player;


    [Header("Horizontal Movement")] 
    private float m_moveSpeed => Player.m_currentMoveSpeed;
    private InputAction m_horizontalInput;

    [Header("Jump")]
    private float m_jumpHeight => Player.m_currentJumpForce;
    [SerializeField] private AnimationCurve m_jumpCurve;
    private InputAction m_jumpInput;
    private bool m_isGrounded;
    private bool m_isMidJump;

    private LayerMask m_groundedMask;
    [SerializeField] private float m_maxJumpDuration;
    private float m_currJumpDuration;
    
    void Start()
    {
        m_horizontalInput = Player.InputActionAsset.FindAction("MoveHorizontal");
        m_jumpInput = Player.InputActionAsset.FindAction("Jump");

        m_PlayerRigidbody = Player.PlayerRigidbody;
        m_PlayerCollider = (Collider2D)Player.PlayerCollider;

        m_groundedMask = LayerMask.GetMask("Floor");
        m_currJumpDuration = 0;

    }
    
    void Update()
    {
        m_isGrounded = m_PlayerCollider.IsTouchingLayers(m_groundedMask);

        if(m_isGrounded && m_jumpInput.WasPressedThisFrame())
        {
            m_isMidJump = true;
        }
        if(m_jumpInput.WasReleasedThisFrame()){
            m_isMidJump = false;
        }

        if(m_isMidJump && m_currJumpDuration <= m_maxJumpDuration){
            m_currJumpDuration += Time.deltaTime;
        }else{
            m_isMidJump = false;
            m_currJumpDuration = 0;
        }
        
    }
    void FixedUpdate()
    {
        m_PlayerRigidbody.linearVelocityX = m_horizontalInput.ReadValue<float>() * m_moveSpeed;
        
        if(m_isMidJump)
        {
            m_PlayerRigidbody.linearVelocityY = m_jumpHeight * m_jumpCurve.Evaluate(m_currJumpDuration);
            
        }
        
    }

}