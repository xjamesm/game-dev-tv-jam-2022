using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private ScreenFader screenFader;
    [SerializeField] private ScreenFader startFader;
    [SerializeField] private ScreenFader loseFader;
    [SerializeField] private ScreenFader endFader;
    [SerializeField] private ScreenFader endGameFader;

    [SerializeField] private ScreenFader menuButtonFader;

    public UnityEvent OnStartLevelEvent;
    public UnityEvent OnNextLevelEvent;
    public UnityEvent OnRetryLevelEvent;
    public UnityEvent OnGotoMenuEvent;

    private ScreenFader currentFader;
    private UnityEvent endEvent;

    private void Start()
    {
        screenFader.gameObject.SetActive(true);
    }

    public void ShowStartScreen()
    {
        menuButtonFader.gameObject.SetActive(true);
        menuButtonFader.FadeSolid();

        screenFader.gameObject.SetActive(true);

        startFader.gameObject.SetActive(true);
        screenFader.SetSolid();
        startFader.FadeSolid();

        loseFader.gameObject.SetActive(false);
        endFader.gameObject.SetActive(false);
        endGameFader.gameObject.SetActive(false);
    }

    public void ShowLostScreen()
    {
        menuButtonFader.gameObject.SetActive(true);
        menuButtonFader.FadeSolid();

        screenFader.gameObject.SetActive(true);

        endFader.gameObject.SetActive(false);
        startFader.gameObject.SetActive(false);
        endGameFader.gameObject.SetActive(false);

        loseFader.gameObject.SetActive(true);

        screenFader.FadeSolid();
        loseFader.FadeSolid();
    }

    public void ShowEndScreen()
    {
        menuButtonFader.gameObject.SetActive(true);
        menuButtonFader.FadeSolid();

        screenFader.gameObject.SetActive(true);

        loseFader.gameObject.SetActive(false);
        startFader.gameObject.SetActive(false);
        endGameFader.gameObject.SetActive(false);

        endFader.gameObject.SetActive(true);
        screenFader.FadeSolid();
        endFader.FadeSolid();
    }

    public void ShowEndGameScreen()
    {
        menuButtonFader.gameObject.SetActive(true);
        menuButtonFader.FadeSolid();

        screenFader.gameObject.SetActive(true);

        loseFader.gameObject.SetActive(false);
        startFader.gameObject.SetActive(false);
        endGameFader.gameObject.SetActive(false);

        endGameFader.gameObject.SetActive(true);
        screenFader.FadeSolid();
        endGameFader.FadeSolid();
    }

    public void OnStartClick()
    {
        currentFader = startFader;
        endEvent = OnStartLevelEvent;
        StartCoroutine(WaitForScreenFader());
    }

    public void OnGameOverRetryClick()
    {
        currentFader = loseFader;
        endEvent = OnRetryLevelEvent;
        StartCoroutine(WaitForCurrentFader());
    }

    public void OnEndLevelRetryClick()
    {
        currentFader = endFader;
        endEvent = OnRetryLevelEvent;
        StartCoroutine(WaitForCurrentFader());
    }

    public void OnEndLevelNextClick()
    {
        currentFader = endFader;
        endEvent = OnNextLevelEvent;
        StartCoroutine(WaitForCurrentFader());
    }

    public void OnRetryClick()
    {
        screenFader.FadeSolid();
        OnRetryLevelEvent?.Invoke();
    }

    public void OnMenuClick()
    {
        OnGotoMenuEvent?.Invoke();
    }

    private IEnumerator WaitForCurrentFader()
    {
        currentFader.FadeClear();
        menuButtonFader.FadeClear();
        yield return new WaitForSeconds(currentFader.FadeClearDuration);
        endEvent?.Invoke();
    }

    private IEnumerator WaitForScreenFader()
    {
        currentFader.FadeClear();
        menuButtonFader.FadeClear();
        yield return new WaitForSeconds(currentFader.FadeClearDuration);

        screenFader.FadeClear();
        yield return new WaitForSeconds(screenFader.FadeClearDuration);

        screenFader.gameObject.SetActive(false);
        endEvent?.Invoke();
        currentFader.gameObject.SetActive(false);
        menuButtonFader.gameObject.SetActive(false);
    }
}
