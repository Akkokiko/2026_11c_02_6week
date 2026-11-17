using TMPro;
using UnityEngine;

public class PotionShrine : Interactable
{
    [SerializeField] private SpriteRenderer m_potionSpriteRenderer;
    [SerializeField] private Recipe m_potionRecipe;

    [SerializeField] private TextMeshProUGUI m_descriptionText;
    [SerializeField] private string m_description;
    
    [SerializeField] private ExplosionEffect m_explosionPrefab;

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
            UIManager.instance.PopupUI.insertPopup(new PopupInfo("NEW RECIPE ADDED",m_potionRecipe.RecipeResult.PotionSprite));
            SpawnExplosion(transform.position);
            
            Color tempColor = m_potionSpriteRenderer.color;
            tempColor.a = 0;
            m_potionSpriteRenderer.color = tempColor;
        }
        
        
    }
    void SpawnExplosion(Vector3 position)
    {
        var ex = Instantiate(m_explosionPrefab,position,Quaternion.identity);
        ex.Puff(35);
    }
}
