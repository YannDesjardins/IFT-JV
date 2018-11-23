using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class VisibilityGraph : MonoBehaviour
{
    private List<GraphNode> visibilityGraph = new List<GraphNode>();
    private List<GraphNode> points = new List<GraphNode>();

    // Use this for initialization
    void Start()
    {
        FindPoints();
        FindLinkedEdges();
    }

    private void FindPoints()
    {
        visibilityGraph.Clear();
        GameObject[] navigation = GameObject.FindGameObjectsWithTag("Navigation");
        //Trouve tous les points possibles:
        for (int i = 0; i < navigation.Length; i++)
        {
            Vector3 p = navigation[i].transform.position;
            points.Add(new GraphNode(p.x, p.z));
        }
    }

    private void FindLinkedEdges()
    {
        while (points.Count > 1)
        {
            int current = points.Count - 1;
            GraphNode currentNode = points[current];
            for (int i = points.Count - 2; i >= 0; i--)
            {

                //raycast vers toute
                RaycastHit hit = Raycast(currentNode.GetNodePosition(), points[i].GetNodePosition());
                //si se rend alors ajoute
                float distance = (points[i].GetNodePosition() - currentNode.GetNodePosition()).magnitude;
                if (hit.distance >= distance)
                {
                    points[i].AddNeighbour(currentNode);
                    currentNode.AddNeighbour(points[i]);
                }
            }
            visibilityGraph.Add(points[current]);
            points.RemoveAt(current);
        }
        visibilityGraph.Add(points[0]);
        points.Clear();
    }

    private RaycastHit Raycast(Vector3 origin, Vector3 destination)
    {
        RaycastHit hit;
        int layerMask = 1 << 9;
        Vector3 direction = (destination - origin).normalized;
        Physics.Raycast(origin, direction, out hit, Mathf.Infinity, layerMask);

        return hit;
    }

    public void AddNodesToCalculate(GraphNode origin, GraphNode dest)
    {
        points.Clear();
        points.Add(origin);
        points.Add(dest);
        FindPoints();
        FindLinkedEdges();
    }

    public GraphNode findGraphNodeByPosition(Vector3 position)
    {
        return visibilityGraph.Find(x => x.GetNodePosition().Equals(position));
    }

    public List<GraphNode> GetNodeList()
    {
        return this.visibilityGraph;
    }
}

