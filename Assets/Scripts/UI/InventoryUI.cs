using System;
using System.Collections.Generic;
using LitMotion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
   public GameObject[] m_potionIcons;
   
   [Header("Gold UI")]
   [SerializeField] private RectTransform m_coinRectTransform;
   [SerializeField] private TextMeshProUGUI m_coinAmount;

   [Header("Diamond UI")]
   [SerializeField] private RectTransform m_diamondRectTransform;
   [SerializeField] private TextMeshProUGUI m_diamondAmount;
   
   
   //Flowers
   [Header("Flowers")]
   public FlowerUICard[] m_flowerCards;

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

   public void updatePotionInventory(Potion[] potions)
   {
       for(int i=0; i<potions.Length; i++)
       {
           Potion potion = potions[i];
           if (null == potion)
           {
               m_potionIcons[i].GetComponent<Image>().sprite = null;
           }
           else
           {
               m_potionIcons[i].GetComponent<Image>().sprite = potion.PotionSprite;
           }
       }
   }
   
   
   //Flowers
   public void updateFlowerCard(Flower flower, int newAmount)
   {
       foreach (FlowerUICard card in m_flowerCards)
       {
           if (card.RepresentedFlower == flower)
           {
               card.updateCountText(newAmount);
               return;
           }
       }
   }


   public void updateCoinsText(int newAmount)
   {
       m_coinAmount.text = newAmount.ToString();    
       LSequence.Create()
           .Append(LMotion.Create(0.9f, 1.5f, 0.12f).WithEase(Ease.InSine) 
               .Bind(x => m_coinRectTransform.localScale = Vector2.one * x)).
           Append(LMotion.Create(1.5f, 1f, 0.1f).WithDelay(0.05f)
               .WithEase(Ease.OutSine).Bind(x =>m_coinRectTransform.localScale = Vector2.one * x)).
           Run();
   }

   public void updateDiamondText(int newAmount)
   {
       m_diamondAmount.text = newAmount.ToString();
       LSequence.Create()
           .Append(LMotion.Create(0.9f, 1.5f, 0.12f).WithEase(Ease.InSine) 
               .Bind(x => m_diamondRectTransform.localScale = Vector2.one * x)).
           Append(LMotion.Create(1.5f, 1f, 0.1f).WithDelay(0.05f)
               .WithEase(Ease.OutSine).Bind(x =>m_diamondRectTransform.localScale = Vector2.one * x)).
           Run();
   }



}
