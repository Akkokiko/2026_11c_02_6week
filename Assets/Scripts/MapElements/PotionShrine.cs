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
        if (player.RecipeList.Contains(m_potionRecipe) == false)
        {
            player.AddKnownRecipe(m_potionRecipe);
            Debug.Log(player);
            player.RecipeBook.showDiscoveredRecipes();
        }
        
        
    }
}
