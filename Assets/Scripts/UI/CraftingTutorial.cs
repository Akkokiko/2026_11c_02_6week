using UnityEngine;

public class CraftingTutorial : MonoBehaviour
{
    bool isFirstTime = true;
    [SerializeField] private GameObject m_tutorialUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isFirstTime = true;
        m_tutorialUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPanel()
    {
        m_tutorialUI.SetActive(true);
    }

    public void OpenFirstTime()
    {
        if (isFirstTime)
        {
            isFirstTime = false;
            OpenPanel();
        }
    }

    public void ClosePanel()
    {
        m_tutorialUI.SetActive(false);
    }
}
