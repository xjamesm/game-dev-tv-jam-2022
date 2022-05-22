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

    public int TurnCounter = 0;

    GameState currentState = GameState.Setup;
    public GameState CurrentState { get => currentState; }

    public bool SavedCorpse;

    public UnityEvent setupEvent;
    public UnityEvent startLevelEvent;
    public UnityEvent playLevelEvent;
    public UnityEvent endLevelEvent;
    public UnityEvent winLevelEvent;
    public UnityEvent loseLevelEvent;

    private Board board;
    private PlayerManager player;
    List<EnemyManager> enemies;

    private void Awake()
    {
        board = FindObjectOfType<Board>();
        player = FindObjectOfType<PlayerManager>();
        var aEnemies = FindObjectsOfType<EnemyManager>() as EnemyManager[];
        enemies = new List<EnemyManager>(aEnemies);
    }

    public void Start()
    {
        SetupLevel();
    }

    public void SetupLevel()
    {
        player.SetInputActive(false);
        currentState = GameState.Setup;
        setupEvent?.Invoke();
    }

    public void StartLevel()
    {
        TurnCounter = 0;
        currentTurn = Turn.Player;

        currentState = GameState.StartLevel;
        startLevelEvent?.Invoke();
    }

    public void PlayLevel()
    {
        player.SetInputActive(true);
        currentState = GameState.GamePlaying;
        playLevelEvent?.Invoke();
    }

    public void EndLevel()
    {
        endLevelEvent?.Invoke();
        player.SetInputActive(false);

        if (SavedCorpse)
        {
            currentState = GameState.GameWin;
            winLevelEvent?.Invoke();
        }
        else
        {
            currentState = GameState.GameOver;
            loseLevelEvent?.Invoke();
        }
    }

    public void UpdateTurn()
    {
        if(currentTurn == Turn.Player)
        {
            TurnCounter++;
            if (player.IsTurnComplete && !AreEnemiesAllDead())
            {
                PlayEnemyTurn();
            }
        }
        else if(currentTurn == Turn.Enemy)
        {
            if(IsEnemyTurnComplete())
            {
                PlayPlayerTurn();
            }
        }
    }

    private bool AreEnemiesAllDead()
    {
        if (enemies == null)
            return true;

        foreach (EnemyManager enemy in enemies)
        {
            if (!enemy.IsDead)
            {
                return false;
            }
        }

        return true;
    }

    private void PlayEnemyTurn()
    {
        currentTurn = Turn.Enemy;

        if (enemies == null)
            return;

        foreach (EnemyManager enemy in enemies)
        {
            if (enemy != null && !enemy.IsDead)
            {
                enemy.IsTurnComplete = false;
                enemy.PlayTurn();
            }
        }
    }

    private bool IsEnemyTurnComplete()
    {
        if (enemies == null)
            return true;

        foreach (EnemyManager enemy in enemies)
        {
            if (enemy.IsDead) continue;
            if (!enemy.IsTurnComplete) return false;
        }

        return true;
    }

    private void PlayPlayerTurn()
    {
        currentTurn = Turn.Player;
        player.IsTurnComplete = false;
    }
}
