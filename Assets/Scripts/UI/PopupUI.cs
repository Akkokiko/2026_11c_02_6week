using System.Collections.Generic;
using LitMotion;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public record  PopupInfo
{
    public string text;
    public Sprite icon;

    public PopupInfo(string text, Sprite icon)
    {
        this.text = text;
        this.icon = icon;
    }
}

public class PopupUI : MonoBehaviour
{
    private RectTransform rectTransform;
    
    [Header("Animation")]
    public float inTime = 0.2f;
    public float stayTime = 5f;
    public float outTime = 0.2f;
    
    [SerializeField] private Vector2 m_offset = new Vector2(0, 0);
    private Vector2 originPosition;
    
    
    [Header("UI")]
    public TextMeshProUGUI text;
    public Image icon;
    
    
    MotionHandle m_popupHandle;
    bool m_isPlaying;
    
    private Queue<PopupInfo>  popupQueue = new Queue<PopupInfo>();


    void Start()
    {
        originPosition = GetComponent<RectTransform>().anchoredPosition;
        rectTransform = GetComponent<RectTransform>();
    }

    public void Popup(PopupInfo info)
    {
        text.text = info.text;
        icon.sprite = info.icon;
            
       rectTransform.anchoredPosition = originPosition;

       m_popupHandle = LSequence.Create()
            .Append(
                LMotion.Create(originPosition, m_offset, inTime)
                    .Bind(x => rectTransform.anchoredPosition = x)
            )
            .Append(
                LMotion.Create(m_offset, originPosition, outTime)
                    .WithDelay(stayTime)
                    .WithOnComplete(() => {
                        nextPopup();
                    })
                    .Bind(x => rectTransform.anchoredPosition = x)
            )
            .Run();
       m_popupHandle.AddTo(gameObject);
    }

    void nextPopup()
    {
        if (m_isPlaying)
        {
            if (popupQueue.Count != 0)
            {
                Popup(popupQueue.Dequeue());
            }
            else
            {
                m_isPlaying = false;
            }
        }
    }

    public void insertPopup(PopupInfo info)
    {
        popupQueue.Enqueue(info);
        if (!m_isPlaying)
        {
            m_isPlaying = true;
            Popup(popupQueue.Dequeue());
        }
    }



}
