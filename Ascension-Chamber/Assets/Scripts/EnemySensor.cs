using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySensor : MonoBehaviour
{
    public Vector3 dirToSearch = new Vector3(0f, 0f, Board.spacing);

    private Node nodeToSearch;
    private Board board;

    private bool foundPlayer = false;
    public bool FoundPlayer { get => foundPlayer; }

    private bool foundCorpse = false;
    public bool FoundCorpse { get => foundCorpse; }

    private void Awake()
    {
        board = FindObjectOfType<Board>();
    }

    public void UpdateSensor(Node currentNode)
    {
        Vector3 worldPosToSearch = transform.TransformVector(dirToSearch) + transform.position;

        if (board != null)
        {
            nodeToSearch = board.FindNodeAt(worldPosToSearch);

            if (!currentNode.LinkedNodes.Contains(nodeToSearch))
            {
                foundPlayer = false;
                return;
            }

            if (nodeToSearch == board.PlayerNode)
            {
                foundPlayer = true;
            }

            if (nodeToSearch == board.CorpseNode)
            {
                foundCorpse = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);

        Gizmos.DrawCube(transform.position + transform.TransformVector(dirToSearch), Vector3.one * 0.6f);
    }
}
