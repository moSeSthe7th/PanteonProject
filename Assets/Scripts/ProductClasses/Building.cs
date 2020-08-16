using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    
   
    protected bool isSelected;

    Sprite sprite;
    

    private void OnEnable()
    {

       
        isSelected = true;
        
        sprite = GetComponent<Sprite>();
    }

   
    protected virtual void SetPosition(Vector2 pos)
    {
        int xPos = Mathf.RoundToInt(pos.x);
        int yPos = Mathf.RoundToInt(pos.y);
        pos.x = xPos;
        pos.y = yPos;
      
        transform.position = pos;
      
    }

    
    //get all covered nodes by the building
    protected virtual Vector2[] GetCoveredNodes()
    {
        Vector2[] coveredNodes = new Vector2[16];

        int bottomLeftXPos = Mathf.RoundToInt(transform.position.x - 1.5f);
        int bottomLeftYPos = Mathf.RoundToInt(transform.position.y - 1.5f);

        int nodePlace = 0;

        for(int i = bottomLeftXPos; i< bottomLeftXPos + 4; i++)
        {
            for(int j = bottomLeftYPos; j < bottomLeftYPos + 4; j++)
            {
                coveredNodes[nodePlace] = new Vector2(i, j);
                //Debug.Log("covered nodes " + i + " " + j);
                nodePlace++;
            }
        }

        return coveredNodes;
    }

    //try to put the building if the position has not occupied
    public bool TryToPut()
    {
        if (CheckIfAreaAvailable())
        {
            //isSelected = false;
            GetComponent<SpriteRenderer>().color = Color.white;
            SetNodesAsOccupied();
            return true;
        }
        return false;
    }

    //check if current area is available to put the building
    public bool CheckIfAreaAvailable()
    {
        foreach(Vector2 node in GetCoveredNodes())
        {
           
            if (Grid.instance.grid[(int)node.x, (int)node.y].isOccupied)
            {
                //Debug.Log("Occupied at botom: " + node.x + " " + node.y);
                return false;
            }
        }
        return true;
    }

    //set covered nodes as occupied
    public void SetNodesAsOccupied()
    {
       
        foreach(Vector2 node in GetCoveredNodes())
        {
            Grid.instance.grid[(int)node.x, (int)node.y].isOccupied = true;
        }
    }

    //carry building with mouse
    public void CarryTheBuilding(Vector2 screenPos)
    {
        if (isSelected)
        {
            SetPosition(screenPos);
            if (CheckIfAreaAvailable())
            {
                GetComponent<SpriteRenderer>().color = Color.green;
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
            
    }
}
