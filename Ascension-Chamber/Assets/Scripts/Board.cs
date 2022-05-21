using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static float spacing = 1f;
    public static readonly Vector2[] directions =
    {
        new Vector2(spacing, 0f),
        new Vector2(-spacing, 0f),
        new Vector2(0f,spacing),
        new Vector2(0f,-spacing),
    };

    List<Node> allNodes = new List<Node>();
    private Node playerNode;
    private Node goalNode;

    public List<Node> AllNodes { get => allNodes; }
    public Node PlayerNode { get => playerNode; }
    public Node GoalNode { get => goalNode; }

    private void Awake()
    {
        GetAllNodes();
    }

    public void InitBoard()
    {
        playerNode = allNodes[0];

        if(playerNode != null)
        {
            playerNode.InitNode(this);
        }
    }

    private void GetAllNodes()
    {
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<Node>(out Node node))
            {
                if(!allNodes.Contains(node))
                {
                    allNodes.Add(node);
                }
            }
        }
    }

    public Node FindNodeAt(Vector3 pos)
    {
        Vector2 boardCoord = Vector2Int.RoundToInt(new Vector2(pos.x, pos.z));
        return allNodes.Find(n => n.Position == boardCoord);
    }
    
    public Node FindGoalNode()
    {
        return allNodes.Find(n => n.isLevelGoal);
    }
}
