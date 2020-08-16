using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private const int moveStraightCost = 10;
    private const int moveDiagonalCost = 14;

    private List<Node> openList;
    private List<Node> closedList;
    
    public List<Node> FindPath(int startX,int startY, int endX, int endY)
    {

        //find start and end nodes
        Node startNode = Grid.instance.grid[startX, startY];
        Node endNode = Grid.instance.grid[endX, endY];

        //create open/close lists
        openList = new List<Node>() { startNode };
        closedList = new List<Node>();

        //initialize g cost and set parent to null of each node
        for(int x = 0; x < DataScript.gridSizeX; x++)
        {
            for(int y = 0; y < DataScript.gridSizeY; y++)
            {
                Node node = Grid.instance.grid[x, y];
                node.gCost = int.MaxValue;
                node.parent = null;
            }
        }

        //calculate costs of start node
        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);

        //while there are non-checked nodes
        while(openList.Count > 0)
        {
            //get the node with the lowest f cost from the open list
            Node currentNode = GetLowestFCostNode(openList);
            if(currentNode == endNode)
            {
                //Reached Final node
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            //check all of the neighbours of the current node and add them to path if suitable
            foreach(Node neighbourNode in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(neighbourNode))
                    continue;

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.gCost && !neighbourNode.isOccupied)
                {
                    neighbourNode.parent = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);

                    if (!openList.Contains(neighbourNode))
                        openList.Add(neighbourNode);
                }
            }
        }

        // couldnt find the path
        return null;
    }

    //get all the neighbours of a node
    private List<Node> GetNeighbourList(Node currentNode)
    {
        List<Node> neighbourList = new List<Node>();

        if (currentNode.gridX - 1 >= 0)
        {
            //Left
            neighbourList.Add(Grid.instance.grid[currentNode.gridX - 1, currentNode.gridY]);
            //Left Down
            if (currentNode.gridY - 1 >= 0)
                neighbourList.Add(Grid.instance.grid[currentNode.gridX - 1, currentNode.gridY - 1]);
            //Left Up
            if (currentNode.gridY + 1 < DataScript.gridSizeY)
                neighbourList.Add(Grid.instance.grid[currentNode.gridX - 1, currentNode.gridY + 1]);
        }
        if (currentNode.gridX + 1 < DataScript.gridSizeX)
        {
            //Right
            neighbourList.Add(Grid.instance.grid[currentNode.gridX + 1, currentNode.gridY]);
            //Right Down
            if (currentNode.gridY - 1 >= 0)
                neighbourList.Add(Grid.instance.grid[currentNode.gridX + 1, currentNode.gridY - 1]);
            //Right Up
            if (currentNode.gridY + 1 < DataScript.gridSizeY)
                neighbourList.Add(Grid.instance.grid[currentNode.gridX + 1, currentNode.gridY + 1]);
        }
        //Down
        if (currentNode.gridY - 1 >= 0)
            neighbourList.Add(Grid.instance.grid[currentNode.gridX, currentNode.gridY - 1]);
        //Up
        if (currentNode.gridY + 1 < DataScript.gridSizeY)
            neighbourList.Add(Grid.instance.grid[currentNode.gridX, currentNode.gridY + 1]);

        return neighbourList;
    }

    //calculate the path with backtracing
    private List<Node> CalculatePath(Node endNode)
    {
        List<Node> path = new List<Node>();
        path.Add(endNode);
        Node currentNode = endNode;
        while(currentNode.parent != null)
        {
            path.Add(currentNode.parent);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        endNode.isOccupied = true;
        path[0].isOccupied = false;
        return path;
    }

    //calculate h cost between two points
    private int CalculateDistanceCost(Node a, Node b)
    {
        int xDistance = Mathf.Abs(a.gridX - b.gridX);
        int yDistance = Mathf.Abs(a.gridY - b.gridY);
        int remaining = Mathf.Abs(xDistance - yDistance);

        return moveDiagonalCost * Mathf.Min(xDistance, yDistance) + moveStraightCost * remaining;
    }

    //get the node with the lowest f cost from a list
    private Node GetLowestFCostNode(List<Node> nodeList)
    {
        Node lowestFCostNode = nodeList[0];
        for(int i = 1; i < nodeList.Count; i++)
        {
            if(nodeList[i].FCost < lowestFCostNode.FCost)
            {
                lowestFCostNode = nodeList[i];
            }
        }
        return lowestFCostNode;
    }

}
