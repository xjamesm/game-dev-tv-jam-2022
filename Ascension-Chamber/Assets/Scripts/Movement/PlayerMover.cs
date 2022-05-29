using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : Mover
{
    protected override IEnumerator MoveCo(Vector3 destinationPos, float delayTime)
    {
        isMoving = true;

        CorpseManager corpse = GetCorpseOnNode(destinationPos);

        if (corpse != null)
        {
            Vector3 relPos = destinationPos - transform.position;

            if (corpse.CanMoveDirection(destinationPos + relPos))
            {
                destination = destinationPos;
                yield return StartCoroutine(FaceDestination());

                yield return StartCoroutine(corpse.Mover.MoveCorpse(destinationPos + relPos));
                onFinishMovementEvent?.Invoke();

                
            }
            isMoving = false;
            yield break;
        }

        yield return base.MoveCo(destinationPos, delayTime);
    }

    public CorpseManager GetCorpseOnNode(Vector3 position)
    {
        Node node = board.FindNodeAt(position);
        if(node != null)
        {
            if (board.CorpseNode != null && node.Position == board.CorpseNode.Position)
            {
                return board.Corpse;
            }
        }
        return null;
    }
}
