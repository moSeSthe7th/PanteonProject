using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Building
{
  
    protected override void SetPosition(Vector2 pos)
    {

        int xPos = Mathf.RoundToInt(pos.x);
        int yPos = Mathf.RoundToInt(pos.y);

        pos.x = xPos + 0.5f;
        pos.y = yPos + 0.5f;

       
        transform.position = pos;
        
    }

    // get all of the nodes covered by that barracks 
    protected override Vector2[] GetCoveredNodes()
    {
        //barracks cover 16 nodes
        Vector2[] coveredNodes = new Vector2[16];

        //calculate bottom left positions and start iterating from there
        int bottomLeftXPos = Mathf.RoundToInt(transform.position.x - 1.5f);
        int bottomLeftYPos = Mathf.RoundToInt(transform.position.y - 1.5f);

        int nodePlace = 0;

        for (int i = bottomLeftXPos; i < bottomLeftXPos + 4; i++)
        {
            for (int j = bottomLeftYPos; j < bottomLeftYPos + 4; j++)
            {
                coveredNodes[nodePlace] = new Vector2(i, j);
               
                nodePlace++;
            }
        }


        return coveredNodes;
    }

    //Finds a suitable spawn point for soldiers
   public Vector2 FindSuitableSpawnPoint()
    {
        Vector2 soldierSpawnPos = (Vector2)transform.position - Vector2.one * 2.5f;
        //if default spawn position is not occupied, spawn soldier at the default position
        if (!Grid.instance.grid[(int)soldierSpawnPos.x, (int)soldierSpawnPos.y].isOccupied)
        {
            return soldierSpawnPos;
        }
        else
        {
            //find the closest point to the default spawn point
            bool isFound = false;
            Vector2 closest = Vector2.positiveInfinity;
            float diffWithClosest = Mathf.Infinity;
            int k = 1;

            while (!isFound)
            {
                //x and y increases as k because of prevent multiple check at smaller k values. The only multiple check here is the 0
                for (int x = k * -1; x <= k; x += k)
                {
                    for (int y = k * -1; y <= k; y += k)
                    {
                        if (!Grid.instance.grid[(int)soldierSpawnPos.x + x, (int)soldierSpawnPos.y + y].isOccupied &&
                            Vector2.SqrMagnitude(new Vector2((int)soldierSpawnPos.x + x, (int)soldierSpawnPos.y + y) - soldierSpawnPos) < diffWithClosest)
                        {
                            closest = new Vector2((int)soldierSpawnPos.x + x, (int)soldierSpawnPos.y + y);
                            diffWithClosest = Vector2.SqrMagnitude(new Vector2((int)soldierSpawnPos.x + x, (int)soldierSpawnPos.y + y) - soldierSpawnPos);
                            isFound = true;
                        }
                    }
                }
                k++;
            }
            return closest;
        }
     
    }

    //spawn soldier at a suitable point and set that point as occupied
    public void SpawnSoldier()
    {
        GameObject soldier = ObjectPooler.instance.GetPooledObject(DataScript.Soldiers);
        if(soldier != null)
        {
            soldier.transform.position = FindSuitableSpawnPoint();
            Grid.instance.grid[(int)soldier.transform.position.x, (int)soldier.transform.position.y].isOccupied = true;
            soldier.SetActive(true);
        }
      
    }
}
