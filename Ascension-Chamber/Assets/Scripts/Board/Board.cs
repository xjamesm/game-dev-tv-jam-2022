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
    public List<Node> AllNodes { get => allNodes; }

    private Node playerNode;
    public Node PlayerNode { get => playerNode; }

    private Node goalNode;
    public Node GoalNode { get => goalNode; }
    
    private Node corpseNode;
    public Node CorpseNode { get => corpseNode; }
    
    private PlayerMover player;

    private CorpseManager corpse;
    public CorpseManager Corpse { get => corpse; }

    private void Awake()
    {
        player = FindObjectOfType<PlayerMover>();
        corpse = FindObjectOfType<CorpseManager>();

        GetAllNodes();
        goalNode = FindGoalNode();
    }

    public void InitBoard()
    {
        UpdatePlayerNode();

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

    public void UpdatePlayerNode()
    {
        playerNode = player.CurrentNode;
    }

    public void UpdateCorpseNode()
    {
        if (Corpse != null)
            corpseNode = Corpse.CurrentNode;
        else
            corpseNode = null;
    }

    public void DestroyCorpseRef()
    {
        corpseNode = null;
        corpse = null;
    }
}
