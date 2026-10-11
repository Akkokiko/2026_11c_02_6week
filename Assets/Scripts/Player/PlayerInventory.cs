using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
   
   [SerializeField] private List<Potion> m_potions = new List<Potion>();
   
   
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

   public Potion GetPotion(int index = 0)
   {
      return m_potions[index];
   }
}
