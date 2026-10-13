using TMPro;
using UnityEngine;

public class FlowerUICard : MonoBehaviour
{

    [SerializeField] private Flower m_representedFlower;
    public Flower RepresentedFlower => m_representedFlower;
    
            
    [SerializeField] private TextMeshProUGUI m_countText;
    

    public void updateCountText(int amount)
    {
        m_countText.text = amount.ToString();
    }
}
