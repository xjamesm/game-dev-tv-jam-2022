using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] protected float solidAlpha = 1f;
    [SerializeField] protected float clearAlpha = 0f;

    [SerializeField] private float fadeSolidDuration = 2f;
    public float FadeSolidDuration { get => fadeSolidDuration; }

    [SerializeField] private float fadeClearDuration = 2f;
    public float FadeClearDuration { get => fadeClearDuration; }

    [SerializeField] private MaskableGraphic[] graphicsToFade;

    protected void SetAlpha(float alpha)
    {
        foreach (MaskableGraphic graphic in graphicsToFade)
        {
            if (graphic != null)
            {
                graphic.canvasRenderer.SetAlpha(alpha);
            }
        }
    }

    private void Fade(float targetAlpha, float duration)
    {
        foreach (MaskableGraphic graphic in graphicsToFade)
        {
            if (graphic != null)
            {
                graphic.CrossFadeAlpha(targetAlpha, duration, true);
            }
        }
    }

    public void FadeClear() // fade from opaque to clear
    {
        SetAlpha(solidAlpha);
        Fade(clearAlpha, FadeClearDuration);
    }

    public void FadeSolid() // fade from clear to opaque
    {
        SetAlpha(clearAlpha);
        Fade(solidAlpha, FadeSolidDuration);
    }

    public void SetClear()
    {
        SetAlpha(clearAlpha);
    }

    public void SetSolid()
    {
        SetAlpha(solidAlpha);
    }
}
