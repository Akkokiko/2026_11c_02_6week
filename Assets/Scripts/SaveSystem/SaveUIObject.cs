using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SaveUIObject : MonoBehaviour
{
    [SerializeField] private TMP_Text m_tmpText;

    void Start()
    {
        SaveData saveData = SaveController.Instance.GetSaveDataFromFile();
        if (saveData == null)
        {
            gameObject.SetActive(false);
        }
        string text = "Coins: " + saveData.CoinAmount
                    + "\nDiamonds: " + saveData.DiamondAmount
                    + "\nLevel: " + SceneManager.GetSceneByBuildIndex(saveData.m_CurrentLevel).name;
        m_tmpText.text = text;
    }

    public void LoadThisSave()
    {
        //obiekt powinien przechowywać swoje save data i uzywać do load game ale system trzeba byłoby zmieniać więc /shrug
        SaveController.Instance.LoadGame();
    }
}
