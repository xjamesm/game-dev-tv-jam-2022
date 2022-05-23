using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : TurnManager
{
    public bool IsDead;
    public bool CanSeeGhost = false;

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

        if(sensor.FoundPlayer && CanSeeGhost)
        {
            Vector3 corpsePos = new Vector3(board.CorpseNode.Position.x, 0f, board.CorpseNode.Position.y);
            mover.Move(corpsePos, 0f);
        }
        else if (sensor.FoundCorpse)
        {
            Vector3 corpsePos = new Vector3(board.CorpseNode.Position.x, 0f, board.CorpseNode.Position.y);
            mover.Move(corpsePos, 0f);
        }
        else
        {
            mover.MoveOneTurn();
            yield break;
        }


        while (mover.isMoving)
        {
            yield return null;
        }

        gameManager.EndLevel();
    }
}
