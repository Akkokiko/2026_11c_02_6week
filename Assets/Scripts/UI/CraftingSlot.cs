using System;

using UnityEngine;
using UnityEngine.UI;

public class CraftingSlot : MonoBehaviour
{
    private Flower m_FlowerInSlot;
    public Flower FlowerInSlot => m_FlowerInSlot;

    [SerializeField] private Image displayedImage;
    [SerializeField] private Sprite defaultSprite; 

    public void putFlowerInSlot(Flower flower)
    {
        m_FlowerInSlot = flower;
        if (flower == null)
        {
            displayedImage.sprite = defaultSprite;
        }
        else
        {
            displayedImage.sprite = flower.sprite;
        }
    }

    void Update()
    {
        if (m_FlowerInSlot != null)
        {
            Debug.Log("Flower is set" + this.gameObject.name);
        }
    }
}
