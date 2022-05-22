using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : TurnManager
{
    public bool IsDead;

    EnemyMover mover;
    EnemySensor sensor;

    protected override void Awake()
    {
        base.Awake();
        mover = GetComponent<EnemyMover>();
        sensor = GetComponent<EnemySensor>();
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

        sensor.UpdateSensor(mover.CurrentNode);

        if (sensor.FoundCorpse)
        {
            Vector3 corpsePos = new Vector3(board.CorpseNode.Position.x, 0f, board.CorpseNode.Position.y);
            mover.Move(corpsePos, 0f);

            while (mover.isMoving)
            {
                yield return null;
            }

            gameManager.EndLevel();
        }
        else
        {
            mover.MoveOneTurn();
        }

    }
}
