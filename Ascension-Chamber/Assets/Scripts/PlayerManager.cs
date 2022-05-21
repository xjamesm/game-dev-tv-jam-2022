using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMover), typeof(PlayerInput))]
public class PlayerManager : TurnManager
{
    public PlayerMover mover;
    public PlayerInput input;

    public UnityEvent onDeathEvent;

    protected override void Awake()
    {
        base.Awake();

        input = GetComponent<PlayerInput>();
        mover = GetComponent<PlayerMover>();

        input.inputEnabled = true;
    }

    private void Update()
    {
        if (mover.isMoving || gameManager.CurrentTurn != Turn.Player)
            return;

        input.GetKeyInput();

        if (input.V == 0)
        {
            if (input.H < 0)
                mover.MoveLeft();
            if (input.H > 0)
                mover.MoveRight();
        }
        else if (input.H == 0)
        {
            if (input.V < 0)
                mover.MoveBackward();
            if (input.V > 0)
                mover.MoveForward();
        }
    }

    public void Die()
    {
        onDeathEvent?.Invoke();
    }
}
