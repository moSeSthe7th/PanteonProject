using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int gridX;
    public int gridY;

    public bool isOccupied;

    public Vector2 position;

    public Node parent;

    public int gCost;
    public int hCost;

    public int FCost { get { return gCost + hCost; } }

    //constructor
    public Node(bool isOccupied, Vector2 position, int gridX, int gridY)
    {
        this.isOccupied = isOccupied;
        this.position = position;
        this.gridX = gridX;
        this.gridY = gridY;
    }

}
