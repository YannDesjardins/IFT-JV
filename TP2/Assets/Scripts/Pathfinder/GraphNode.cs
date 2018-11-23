using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphNode
{

    private Vector3 nodePosition;

    private List<GraphNode> neighbours;

    private float distance;

    private GraphNode parent;

    public float Distance
    {
        get
        {
            return distance;
        }

        set
        {
            distance = value;
        }
    }

    public GraphNode Parent
    {
        get
        {
            return parent;
        }

        set
        {
            parent = value;
        }
    }

    public GraphNode(float x, float z)
    {
        nodePosition = new Vector3(x, 0, z);
        neighbours = new List<GraphNode>();
    }

    public Vector3 GetNodePosition()
    {
        return this.nodePosition;
    }

    public void SetNodePosition(Vector3 position)
    {
        this.nodePosition = position;
    }

    public List<GraphNode> GetNeighbours()
    {
        return this.neighbours;
    }

    public void AddNeighbour(GraphNode node)
    {
        if (node != this)
        {
            neighbours.Add(node);
        }
    }

    public void RemoveNeighbour(GraphNode node)
    {
        neighbours.Remove(node);
    }

    public float calculateEuclidianDistance(GraphNode other)
    {
        return (this.nodePosition - other.GetNodePosition()).magnitude;
    }

}