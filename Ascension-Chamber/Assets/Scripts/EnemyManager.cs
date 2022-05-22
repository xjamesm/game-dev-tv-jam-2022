using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : TurnManager
{
    public bool IsDead;

    EnemyMover mover;

    protected override void Awake()
    {
        base.Awake();
        mover = GetComponent<EnemyMover>();
    }

    public void PlayTurn()
    {
        if(IsDead)
        {
            FinishTurn();
            return;
        }
        StartCoroutine(PlayTurnCo());
    }

    IEnumerator PlayTurnCo()
    {
        if (gameManager == null || gameManager.CurrentState != GameState.GamePlaying)
            yield break;

        mover.MoveOneTurn();
    }
}
