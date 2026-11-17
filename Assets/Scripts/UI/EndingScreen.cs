using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScreen : MonoBehaviour
{


    public void BackToTitle(SceneAsset sceneAsset)
    {
        SceneManager.LoadScene(sceneAsset.name);
    }
}
