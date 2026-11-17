using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScreen : MonoBehaviour
{


    public void BackToTitle(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
}
