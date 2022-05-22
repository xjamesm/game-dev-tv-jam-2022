using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mover : MonoBehaviour
{
    public Vector3 destination;
    public bool isMoving = false;
    public bool faceDestination = false;

    public float moveSpeed = 1.5f;
    public float delay = 0f;
    public float rotateSpeed = 0.5f;

    protected Board board;
    protected Node currentNode;
    public Node CurrentNode { get => currentNode; }

    public UnityEvent onFinishMovementEvent;

    virtual protected void Awake()
    {
        board = FindObjectOfType<Board>();
    }

    virtual protected void Start()
    {
        UpdateCurrentNode();
    }

    public void Move(Vector3 destinationPos, float delayTime = 0.0f)
    {
        if (isMoving)
            return;

        if (board == null)
            return;

        if(CanMoveToPosition(destinationPos))
        {
            StartCoroutine(MoveCo(destinationPos, delayTime));
        }
    }

    public bool CanMoveToPosition(Vector3 destinationPos)
    {
        Node targetNode = board.FindNodeAt(destinationPos);
        if (targetNode != null && currentNode != null)
        {
            if (currentNode.LinkedNodes.Contains(targetNode))
            {
                return true;
            }
        }
        return false;
    }

    protected virtual IEnumerator MoveCo(Vector3 destinationPos, float delayTime)
    {
        isMoving = true;
        destination = destinationPos;

        if(faceDestination)
        {
            yield return FaceDestination();
        }

        yield return new WaitForSeconds(delayTime);

        float dist = Vector3.Distance(transform.position, destinationPos);
        while (Vector3.Distance(transform.position, destinationPos) > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, destinationPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        isMoving = false;
        transform.position = destinationPos;

        UpdateCurrentNode();
        onFinishMovementEvent?.Invoke();
    }

    public void MoveLeft()
    {
        Vector3 newPos = transform.position + new Vector3(-Board.spacing, 0, 0);
        Move(newPos, delay);
    }

    public void MoveRight()
    {
        Vector3 newPos = transform.position + new Vector3(Board.spacing, 0, 0);
        Move(newPos, delay);
    }

    public void MoveForward()
    {
        Vector3 newPos = transform.position + new Vector3(0, 0, Board.spacing);
        Move(newPos, delay);
    }

    public void MoveBackward()
    {
        Vector3 newPos = transform.position + new Vector3(0, 0, -Board.spacing);
        Move(newPos, delay);
    }

    protected void UpdateCurrentNode()
    {
        if (board != null)
        {
            currentNode = board.FindNodeAt(transform.position);
        }
    }

    protected IEnumerator FaceDestination()
    {
        Vector3 relPos = destination - transform.position;
        Quaternion newRot = Quaternion.LookRotation(relPos, Vector3.up);

        float startY = transform.eulerAngles.y;
        float endY = newRot.eulerAngles.y;

        float time = Mathf.Abs(startY - endY) / rotateSpeed;
        float t = 0.0f;

        while (t < time)
        {
            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startY, endY, t / time);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);

            yield return null;
        }

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, endY, transform.eulerAngles.z);
    }
}
