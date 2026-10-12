using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
   public GameObject[] m_potionIcons;

   public void Awake()
   {
      foreach (GameObject potionIcon in m_potionIcons)
      {
          potionIcon.GetComponent<Outline>().enabled = false;
      }
   }


   public void chosePotion(int slot)
   {
       foreach (GameObject potionIcon in m_potionIcons)
       {
           potionIcon.GetComponent<Outline>().enabled = false;
       }
       m_potionIcons[slot].GetComponent<Outline>().enabled = true;
   }
}
