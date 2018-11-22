using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyPathfinder : MonoBehaviour
{

    public GameObject visibilityGraphObject;

    private VisibilityGraph visibilityGraph;
    private List<GraphNode> front;
    private List<GraphNode> visitedNodes;
    private GraphNode originNode;
    private GraphNode destNode;


    //param point arrivee, point depart, graphe de visibilite (point et voisin) 
    public List<Vector3> DijkstraFindPath(Vector3 origin, Vector3 dest)
    {
        front = new List<GraphNode>();
        visitedNodes = new List<GraphNode>();
        //Ajouter arrivee et fin dans le graphe
        originNode = new GraphNode(origin.x, origin.z);
        destNode = new GraphNode(dest.x, dest.z);

        visibilityGraph = visibilityGraphObject.GetComponent<VisibilityGraph>();
        visibilityGraph.AddNodesToCalculate(originNode, destNode);
        List<GraphNode> graph = visibilityGraph.GetNodeList();
        //setup des nodes
        foreach (GraphNode n in graph)
        {
            n.Distance = float.PositiveInfinity;
            n.Parent = null;
        }

        //ajoute premier point dans pile frontier
        originNode.Distance = 0;
        front.Add(originNode);
        GraphNode current = originNode;
        //Rouler tant que frontier n'est pas vide
        while (front.Count != 0)
        {
            current = front[0];
            if (current == destNode)
            {
                return BuildPath();
            }
            //pour chaque voisin, l'ajouter dans frontier si non visite
            List<GraphNode> neighbours = current.GetNeighbours();
            foreach (GraphNode neighbour in neighbours)
            {
                if (!visitedNodes.Contains(neighbour))
                {

                    if (!front.Contains(neighbour))
                    {
                        front.Add(neighbour);
                    }
                    float currentDistance = neighbour.calculateEuclidianDistance(current) + current.Distance;
                    if (neighbour.Distance > currentDistance)
                    {
                        neighbour.Distance = currentDistance;
                        neighbour.Parent = current;
                    }
                }
            }
            front = front.OrderBy(x => x.Distance).ToList();
            front.Remove(current);
            visitedNodes.Add(current);
        }
        return new List<Vector3>();

    }

    public List<Vector3> BuildPath()
    {
        List<Vector3> path = new List<Vector3>();
        GraphNode node = destNode;
        while (node.Parent != null)
        {
            path.Add(node.GetNodePosition());
            node = node.Parent;
        }
        path.Reverse();
        return path;
    }
}