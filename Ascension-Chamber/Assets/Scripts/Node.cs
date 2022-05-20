using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public float initDelay = 1f;
    public LayerMask obstacleLayer;

    private Vector2 position;
    public Vector2 Position { get => position; }
    
    private List<Node> neighbourNodes = new List<Node>();
    public List<Node> NeighbourNodes { get => neighbourNodes; }
    
    private List<Node> linkedNodes = new List<Node>();
    public List<Node> LinkedNodes { get => linkedNodes; }

    private Board board;
    private bool isInitialised = false;

    public bool isLevelGoal = false;

    public void InitNode(Board _board)
    {
        if(!isInitialised)
        {
            board = _board;
            isInitialised = true;
            InitNeighbours();
        }
    }

    private void InitNeighbours()
    {
        neighbourNodes = FindNighbours(board.AllNodes);
        StartCoroutine(InitNeighboursCo());
    }

    public List<Node> FindNighbours(List<Node> nodes)
    {
        List<Node> nList = new List<Node>();

        foreach (Vector2 dir in Board.directions)
        {
            Node foundNeighbour = FindNeighbourAt(nodes, dir);
            if (foundNeighbour != null && !nList.Contains(foundNeighbour))
                nList.Add(foundNeighbour);
        }

        return nList;
    }

    public Node FindNeighbourAt(List<Node> nodes, Vector2 dir)
    {
        return nodes.Find(n => n.Position == Position + dir);
    }

    public Node FindNeighbourAt(Vector2 dir)
    {
        return FindNeighbourAt(NeighbourNodes, dir);
    }

    IEnumerator InitNeighboursCo()
    {
        yield return new WaitForSeconds(initDelay);

        foreach (Node n in neighbourNodes)
        {
            if (linkedNodes.Contains(n))
                continue;

            Obstacle obstacle = FindObstacle(n);
            if (obstacle == null)
            {
                LinkNode(n);
                n.InitNode(board);
            }
        }
    }

    void LinkNode(Node target)
    {
        if(!linkedNodes.Contains(target))
        {
            linkedNodes.Add(target);
        }

        if(!target.linkedNodes.Contains(this))
        {
            target.linkedNodes.Add(this);
        }
    }

    Obstacle FindObstacle(Node targetNode)
    {
        Vector3 checkDirection = targetNode.transform.position - transform.position;
        RaycastHit raycastHit;

        if (Physics.Raycast(transform.position, checkDirection, out raycastHit, Board.spacing + 0.1f, obstacleLayer))
        {
            return raycastHit.collider.GetComponent<Obstacle>();
        }

        return null;
    }
}
