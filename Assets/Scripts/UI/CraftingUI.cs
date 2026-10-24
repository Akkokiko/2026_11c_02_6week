using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
using System;

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

    [SerializeField] private CraftingSlot[] m_craftingSlots;

    [SerializeField] private RecipeBook m_recipeBook;

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
            //TODO: rewrite this segment so it isn't 3 raycasts but one raycasts that checks for stuff
           FlowerUICard clickedCard = GetClickedFlowerUICard();
            if(clickedCard != null)
            {
                m_heldFlower = clickedCard.RepresentedFlower;
                    
            }
            clickedOnRecipe();
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
    void OnDisable()
    {
        if (m_heldFlower != null)
        {
            m_Player.PlayerInventory.AddFlower(m_heldFlower,1);
            m_heldFlower = null;
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
    private void clickedOnRecipe()
    {
        if (m_Raycaster == null || EventSystem.current == null) return;

        
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();

        m_Raycaster.Raycast(pointerData, results);

        foreach (RaycastResult result in results)
        {
            string resultObjectName = result.gameObject.name;
            if (resultObjectName.ToLower().Contains("recipe"))
            {
                //TODO: refund reset, do separate function.
                foreach (CraftingSlot slot in m_craftingSlots)
                {
                    Flower currFlower = slot.FlowerInSlot;
                    if (currFlower != null)
                    {
                        m_Player.PlayerInventory.AddFlower(currFlower, 1);
                        slot.putFlowerInSlot(null);
                    }
                }

                for (int i = 0; i < 3; i++)
                {
                    Transform flower = result.gameObject.transform.Find("FlowerSlot"+(i+1));
                    if (flower == null)
                    {
                        break;
                    }//needed to avoid nullref
                    Sprite flowerSprite = flower.GetComponent<Image>().sprite;
                    if (flowerSprite == null)
                    {
                        break;
                    }
                    string flowerSpriteName = flowerSprite.name.ToLower();
                    
                    if (flowerSpriteName.Contains("blue"))
                    {
                        if (m_Player.PlayerInventory.TryTakeFlower(m_flowerList[0].RepresentedFlower))
                        {
                            m_craftingSlots[i].putFlowerInSlot(m_flowerList[0].RepresentedFlower);
                        }
                    }else if (flowerSpriteName.Contains("green"))
                    {
                        if (m_Player.PlayerInventory.TryTakeFlower(m_flowerList[1].RepresentedFlower))
                        {
                            m_craftingSlots[i].putFlowerInSlot(m_flowerList[1].RepresentedFlower);
                        }
                    }else if (flowerSpriteName.Contains("seth"))
                    {
                        if (m_Player.PlayerInventory.TryTakeFlower(m_flowerList[2].RepresentedFlower))
                        {
                            m_craftingSlots[i].putFlowerInSlot(m_flowerList[2].RepresentedFlower);
                        }
                    }
                    
                }
                break; // first recipe only
            }
        }

        return;
    }

    public void CraftPotion()
    {
        List<Flower> flowersInCraftSlots = new List<Flower>();
        foreach (CraftingSlot slot in m_craftingSlots)
        {
            Flower currFlower = slot.FlowerInSlot;
            if (currFlower != null)
            {
                flowersInCraftSlots.Add(currFlower);
            }
        }

        Recipe recipe;
        if (!m_Player.PlayerInventory.isFull() && (recipe = m_recipeBook.evaluateRecipe(flowersInCraftSlots)) != null)
        {
            m_Player.PlayerInventory.addPotion(recipe.RecipeResult);
            recipe.markAsDiscovered();
            m_recipeBook.showDiscoveredRecipes();
        }
        else
        {
            //refund
            foreach (Flower flower in flowersInCraftSlots)
            {
                m_Player.PlayerInventory.AddFlower(flower, 1);
            }
        }

        //clear slots
        foreach (CraftingSlot slot in m_craftingSlots)
        {
            slot.putFlowerInSlot(null);
        }
    }
}
