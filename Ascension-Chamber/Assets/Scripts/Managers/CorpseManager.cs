using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CorpseManager : TurnManager
{
    
    protected Node currentNode;
    public Node CurrentNode { get => currentNode; }


    protected CorpseMover mover;
    public CorpseMover Mover { get => mover; }

    public UnityEvent onGoalEvent;
    public UnityEvent onDeathEvent;

    override protected void Awake()
    {
        base.Awake();
        mover = GetComponent<CorpseMover>();
    }

    virtual protected void Start()
    {
        UpdateCurrentNode();
    }

    public void UpdateCurrentNode()
    {
        if (board != null)
        {
            currentNode = board.FindNodeAt(transform.position);
            board.UpdateCorpseNode();
        }
    }

    public bool CanMoveDirection(Vector3 destinationPos)
    {
        if (mover.isMoving)
            return false;

        return Mover.CanMoveToPosition(destinationPos);
    }

    public void EndMovement()
    {
        UpdateCurrentNode();

        if(board.CorpseNode.Position == board.GoalNode.Position)
        {
            gameManager.SavedCorpse = true;
            onGoalEvent?.Invoke(); // Corpse is on the goal node!
        }
    }
}
