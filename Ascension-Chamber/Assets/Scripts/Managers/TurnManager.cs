using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    protected GameManager gameManager;
    protected Board board;
    
    protected bool isTurnComplete = false;
    public bool IsTurnComplete { get => isTurnComplete; set => isTurnComplete = value; }

    virtual protected void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        board = FindObjectOfType<Board>();
    }

    virtual public void FinishTurn()
    {
        isTurnComplete = true;
        gameManager?.UpdateTurn();
    }
}
