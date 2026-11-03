using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinRequirement : Interactable
{
    [Header("Level Requirements")]
    [SerializeField] private int requiredCoins = 10;

    [Header("Next Level")]
    [SerializeField] private string nextLevelName;

    [Header("Dialogue")]
    [TextArea(2, 5)]
    [SerializeField] private string enoughCoinsDialogue =
        "Zebrałeś wystarczająco monet. Możemy iść dalej.";

    [TextArea(2, 5)]
    [SerializeField] private string notEnoughCoinsDialogue =
        "BIEDA! Bieda w chuj";

    public override void Interact(Player player)
    {
        int playerCoins = player.PlayerInventory.m_coinAmount;

        if (playerCoins >= requiredCoins)
        {
            Debug.Log(enoughCoinsDialogue);

            SceneManager.LoadScene(nextLevelName);
        }
        else
        {
            int missingCoins = requiredCoins - playerCoins;

            Debug.Log(notEnoughCoinsDialogue +
                      " Brakuje " + missingCoins + " monet");
        }
    }
}
