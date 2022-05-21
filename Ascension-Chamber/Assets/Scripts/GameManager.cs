using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[System.Serializable]
public enum Turn
{
    Player,
    Enemy
}

[System.Serializable]
public enum GameState
{
    Setup,
    StartLevel,
    GamePlaying,
    GameWin,
    GameOver
}

public class GameManager : MonoBehaviour
{
    Turn currentTurn = Turn.Player;
    public Turn CurrentTurn { get => currentTurn; }

    GameState currentState = GameState.Setup;
    public GameState CurrentState { get => currentState; }

    public UnityEvent setupEvent;
    public UnityEvent startLevelEvent;
    public UnityEvent playLevelEvent;
    public UnityEvent endLevelEvent;
    public UnityEvent winLevelEvent;
    public UnityEvent loseLevelEvent;

    private Board board;

    private void Awake()
    {
        board = FindObjectOfType<Board>(); // Will replace this at some point.
    }

    public void Start()
    {
        SetupLevel();
    }

    public void SetupLevel()
    {
        setupEvent?.Invoke();
        currentState = GameState.Setup;
    }

    public void StartLevel()
    {
        startLevelEvent?.Invoke();
        currentState = GameState.StartLevel;
    }

    public void PlayLevel()
    {
        playLevelEvent?.Invoke();
        currentState = GameState.GamePlaying;
    }

    public void EndLevel()
    {
        endLevelEvent?.Invoke();
        
        // Some logic here
        // invoke win or loose.
    }

    public void UpdateTurn()
    {
        if(currentTurn == Turn.Player)
        {

        }
        else
        {

        }
    }
}
