using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    protected GameManager gameManager;
    private bool isTurnComplete = false;
    protected bool IsTurnComplete { get => isTurnComplete; set => isTurnComplete = value; }

    virtual protected void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    virtual public void FinishTurn()
    {
        isTurnComplete = true;
        gameManager?.UpdateTurn();
    }
}
