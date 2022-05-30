using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnCounterUpdater : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    GameManager gameManager;
    int currentTurn = 1;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if(gameManager.TurnCounter != currentTurn)
        {
            currentTurn = gameManager.TurnCounter;
            text.text = "Turns taken: " + currentTurn;
        }
    }
}
