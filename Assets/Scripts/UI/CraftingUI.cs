using UnityEngine;
using UnityEngine.InputSystem;

public class CraftingUI : MonoBehaviour
{
    [SerializeField] private Player m_Player;
    [SerializeField] private GameObject m_CraftingUI;
    private InputAction m_OpenCraftingUIInput;
    
    void Start()
    {
        m_OpenCraftingUIInput = m_Player.InputActionAsset.FindAction("OpenCraftingUI");
    }

    // Update is called once per frame
    void Update()
    {
        if (m_OpenCraftingUIInput.WasPressedThisFrame())
        {
            m_CraftingUI.SetActive(!m_CraftingUI.activeSelf);
            m_Player.swapIsInMenuState();
        }
    }
}
