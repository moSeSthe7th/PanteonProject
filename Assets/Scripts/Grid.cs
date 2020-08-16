using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
   
    public static Grid instance = null;

    public Node[,] grid;
    Transform tiles;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        tiles = new GameObject("Tiles").transform;
        CreateGrid();
        Debug.Log(DataScript.gridSizeX + " " + DataScript.gridSizeY);
    }

    //initialize nodes and create grid. All of the nodes are not occupied at the start
    void CreateGrid()
    {
        grid = new Node[DataScript.gridSizeX, DataScript.gridSizeY];
        bool isOccupied = false;

        for (int i = 0; i < DataScript.gridSizeX; i++)
        {
            for (int j = 0; j < DataScript.gridSizeY; j++)
            {
                grid[i, j] = new Node(isOccupied, new Vector2(i, j), i, j);
                Instantiate(Resources.Load<GameObject>("Prefabs/Tile"), new Vector2(i, j), Quaternion.identity, tiles);
            }
        }
    }

    public void SetNodeAsOccupied(Vector2[] occupiedPos)
    {
        foreach(Vector2 pos in occupiedPos)
        {
            grid[(int)pos.x, (int)pos.y].isOccupied = true;
        }
    }
}
