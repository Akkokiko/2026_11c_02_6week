using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header(("New game"))]
    [SerializeField] private SceneAsset newGameScene;




    public void NewGame()
    {
        SceneManager.LoadScene(newGameScene.name);
    }
    
        
}
