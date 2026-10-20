using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
   
   
   [SerializeField] private Potion[] m_potions = new Potion[3];
   
   
   [SerializeField] private Player m_player;
   public Player Player => m_player;
   
   //Later this dictionary will use Flower object as a key
   private Dictionary<Flower, int> m_flowerInventory = new Dictionary<Flower, int>();


   public int m_coinAmount = 0;
   
   public void Awake()
   {
      
      UIManager.instance.InventoryUI.updatePotionInventory(m_potions);  
      
   }

   //Function adding Item to inventory
   public void AddFlower(Flower key, int amount)
   {
      if (!m_flowerInventory.TryAdd(key, amount))
      {
         m_flowerInventory[key] += amount;
      }
      UIManager.instance.InventoryUI.updateFlowerCard(key, m_flowerInventory[key]);
   }
   public bool TryTakeFlower(Flower key)
   {
      if (m_flowerInventory[key] > 0)
      {
         m_flowerInventory[key]--;
         UIManager.instance.InventoryUI.updateFlowerCard(key, m_flowerInventory[key]);
         return true;
      }
      return false;
   }

   //Potion Inventory

   public int getPotionInventorySize()
   {
      return m_potions.Length;
   }

   public Potion GetPotion(int index = 0)
   {
      return m_potions[index];   
   }

   public bool isSlotEmpty(int slotIndex)
   {
      Debug.Log("isSlotEmpty: " +  !m_potions[slotIndex] );
      return !m_potions[slotIndex]; ;
   }

   public void addPotion(Potion potion)
   {
      for (int i = 0; i < m_potions.Length; i++)
      {
         if (isSlotEmpty(i))
         {
            m_potions[i] = potion;
            UIManager.instance.InventoryUI.updatePotionInventory(m_potions);
            return;
         }
      }
      UIManager.instance.InventoryUI.updatePotionInventory(m_potions);
   }

   public void removePotion(int slotIndex)
   {
      if (!isSlotEmpty(slotIndex))
      {
         m_potions[slotIndex] = null;
         for (int i = slotIndex + 1; i < m_potions.Length; i++)
         {
            m_potions[i-1] = m_potions[i];
         }
         m_potions[^1] = null;
         UIManager.instance.InventoryUI.updatePotionInventory(m_potions);
      }
   }
   public bool isFull()
   {
      for (int i = 0; i < m_potions.Length; i++)
      {
         if (isSlotEmpty(i))
         {
            return false;
         }
      }
      return true;
   }
   

   public void addCoin(int amount)
   {
      m_coinAmount += amount;
      Debug.Log("Mamy " + m_coinAmount + " coins");
      //UI update
   }
}
