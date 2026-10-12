using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
   
   
   [SerializeField] private Potion[] m_potions = new Potion[3];
   
   
   [SerializeField] private Player m_player;
   public Player Player => m_player;
   
   //Later this dictionary will use Flower object as a key
   private Dictionary<String, int> m_inventory = new Dictionary<String, int>();


   public void Awake()
   {
      m_inventory.Add("Blue Flowers",0);
      m_inventory.Add("Red Flowers", 0);
      m_inventory.Add("Green Flowers", 0);
   }

   //Function adding Item to inventory
   public void AddItem(String key, int amount)
   {
      if (m_inventory.ContainsKey(key))
      {
         m_inventory[key] += amount;
         return;
      }
      m_inventory.Add(key, amount);
      
   }

   //Potion Inventory

   public int getPotionInventorySize()
   {
      return m_inventory.Count;
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
            return;
         }
      }
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
      }
   }
   
   
}
