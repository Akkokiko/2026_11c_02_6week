using UnityEngine;


[CreateAssetMenu(fileName = "New Flower", menuName = "Flower")]
public class Flower : ScriptableObject
{
    [SerializeField] private string m_name;
    [SerializeField] private Sprite m_sprite;
    public Sprite sprite => m_sprite;
    
}
