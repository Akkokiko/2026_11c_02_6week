using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class CraftingUI : MonoBehaviour
{
    [SerializeField] private Player m_Player;
    [SerializeField] private GameObject m_CraftingUI;
    [SerializeField] private GameObject m_InventoryUI;
    private FlowerUICard[] m_flowerList;
    private InputAction m_OpenCraftingUIInput;
    
    [SerializeField] private GraphicRaycaster m_Raycaster;
    private InputAction m_menuInteractInput;
    private Flower m_heldFlower;

    void Start()
    {
        m_OpenCraftingUIInput = m_Player.InputActionAsset.FindAction("OpenCraftingUI");
        m_flowerList = m_InventoryUI.GetComponentsInChildren<FlowerUICard>();
        
        m_menuInteractInput = m_Player.InputActionAsset.FindAction("Fire");
        m_heldFlower = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_OpenCraftingUIInput.WasPressedThisFrame())
        {
            m_CraftingUI.SetActive(!m_CraftingUI.activeSelf);
            m_Player.swapIsInMenuState();
        }

        if (m_CraftingUI.activeSelf && m_menuInteractInput.WasPressedThisFrame())
        {
           FlowerUICard clickedCard = GetClickedFlowerUICard();
            if(clickedCard != null)
            {
                m_heldFlower = clickedCard.RepresentedFlower;
                    
            }
        }
        if (m_CraftingUI.activeSelf && m_menuInteractInput.WasReleasedThisFrame())
        {
            if (m_heldFlower != null)
            {
                CraftingSlot craftingSlot = GetCraftingSlot();
                if (craftingSlot != null)
                {
                    craftingSlot.putFlowerInSlot(m_heldFlower);
                }
                else
                {
                    m_Player.PlayerInventory.AddFlower(m_heldFlower, 1);
                }
                m_heldFlower = null;
            }
        }
    }
    private FlowerUICard GetClickedFlowerUICard()
    {
        if (m_Raycaster == null || EventSystem.current == null) return null;

        
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();

        m_Raycaster.Raycast(pointerData, results);

        foreach (RaycastResult result in results)
        {
            FlowerUICard card = result.gameObject.GetComponentInParent<FlowerUICard>();
            
            //taking flower logic is here
            if (card != null && m_flowerList.Contains(card) && m_Player.PlayerInventory.TryTakeFlower(card.RepresentedFlower))
            {
                return card;
            }
        }

        return null;
    }

    private CraftingSlot GetCraftingSlot()
    {
        if (m_Raycaster == null || EventSystem.current == null) return null;

        
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();

        m_Raycaster.Raycast(pointerData, results);

        foreach (RaycastResult result in results)
        {
            CraftingSlot craftingSlot = result.gameObject.GetComponentInParent<CraftingSlot>();
            
            if (craftingSlot != null)
            {
                return craftingSlot;
            }
        }

        return null;
    }

    void OnDisable()
    {
        if (m_heldFlower != null)
        {
            m_Player.PlayerInventory.AddFlower(m_heldFlower,1);
            m_heldFlower = null;
        }
    }
}
