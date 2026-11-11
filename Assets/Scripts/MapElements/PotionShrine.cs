using TMPro;
using UnityEngine;

public class PotionShrine : Interactable
{
    [SerializeField] private SpriteRenderer m_potionSpriteRenderer;
    [SerializeField] private Recipe m_potionRecipe;

    [SerializeField] private TextMeshProUGUI m_descriptionText;
    [SerializeField] private string m_description;

    protected override void Start()
    {       
        base.Start();

        m_potionSpriteRenderer.sprite = m_potionRecipe.RecipeResult.PotionSprite;
        m_descriptionText.text = m_description;
        
    }

    public override void Interact(Player player)
    {
        //Sprawdź czy gracz ma już tą recipe
        //Jeśli nie to dodaj ja
        
        
    }
}
