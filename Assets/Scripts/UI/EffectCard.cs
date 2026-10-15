using UnityEngine;
using UnityEngine.UI;

using LitMotion;
using LitMotion.Extensions;

public class EffectCard : MonoBehaviour
{
    [SerializeField] private RectTransform m_rectTransform;
    [SerializeField] private Image m_effectDurationBar;
    [SerializeField] private Image m_effectIconImage;
    
    MotionHandle m_durationBarMotionHandle;
    
    public void StartEffect(Effect effect)
    {


        LSequence.Create()
            .Append(LMotion.Create(0, 1.5f, 0.12f).WithEase(Ease.InSine) 
                .Bind(x => m_rectTransform.localScale = Vector2.one * x)).
            Append(LMotion.Create(1.5f, 1f, 0.1f).WithDelay(0.05f)
                .WithEase(Ease.OutSine).Bind(x => m_rectTransform.localScale = Vector2.one * x)).
            Run();
        
        m_durationBarMotionHandle =
            LMotion.Create(1.0f, 0.0f, effect.Duration).WithOnComplete(() => stopEffect())
                .Bind(x => m_effectDurationBar.fillAmount = x);
        
       m_effectIconImage.sprite = effect.m_effectIcon;
    }
    

    public void stopEffect()
    {
        Destroy(gameObject);
    }





}
