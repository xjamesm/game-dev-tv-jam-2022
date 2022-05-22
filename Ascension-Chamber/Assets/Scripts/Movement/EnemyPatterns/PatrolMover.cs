using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMover : EnemyMover
{
    public Vector3 dirToMove = new Vector3(0f, 0f, Board.spacing);

    public override void MoveOneTurn()
    {
        StartCoroutine(PatrolCo());
    }

    IEnumerator PatrolCo()
    {
        Vector3 startPos = new Vector3(CurrentNode.Position.x, transform.position.y, CurrentNode.Position.y);
        Vector3 newDest = startPos + transform.TransformVector(dirToMove);
        Vector3 nextDest = startPos + transform.TransformVector(dirToMove * 2);

        this.Move(newDest, 0f);

        while (isMoving)
        {
            yield return null;
        }

        if (board != null)
        {
            Node newDestNode = board.FindNodeAt(newDest);
            Node nextDestNode = board.FindNodeAt(nextDest);

            if (nextDestNode == null || !newDestNode.LinkedNodes.Contains(nextDestNode))
            {
                destination = startPos;
                yield return FaceDestination();
            }
        }

        base.onFinishMovementEvent.Invoke();
    }
}
