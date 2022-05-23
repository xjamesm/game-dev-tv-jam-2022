using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandMover : EnemyMover
{
    public float standTime = 0.5f;

    public override void MoveOneTurn()
    {
        StartCoroutine(StandCo());
    }

    IEnumerator StandCo()
    {
        yield return new WaitForSeconds(standTime);
        base.onFinishMovementEvent.Invoke();
    }
}
