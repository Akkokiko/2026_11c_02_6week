using UnityEngine;

public class CraftingSlot : MonoBehaviour
{
    private Flower m_FlowerInSlot;
    public Flower FlowerInSlot => m_FlowerInSlot;

    public void putFlowerInSlot(Flower flower)
    {
        m_FlowerInSlot = flower;
    }

    void Update()
    {
        if (m_FlowerInSlot != null)
        {
            Debug.Log("Flower is set" + this.gameObject.name);
        }
    }
}
