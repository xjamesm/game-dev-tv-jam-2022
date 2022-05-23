using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMover : EnemyMover
{
    public List<Vector3> lookdirections;

    private int currentDirection = 0;

    public override void MoveOneTurn()
    {
        StartCoroutine(RotateCo());
    }

    IEnumerator RotateCo()
    {
        currentDirection++;
        if (currentDirection >= lookdirections.Count)
            currentDirection = 0;

        destination = lookdirections[currentDirection] + transform.position;
        yield return FaceDestination();

        onFinishMovementEvent?.Invoke();
    }
}
