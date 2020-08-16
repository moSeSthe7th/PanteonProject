using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlant : Building
{
    //override of  parent method
   protected override void SetPosition(Vector2 pos)
    {
     
        int xPos = Mathf.RoundToInt(pos.x);
        int yPos = Mathf.RoundToInt(pos.y);

        pos.x = xPos + 0.5f;
        pos.y = yPos;

        
        transform.position = pos;
       
    }

    //override of parent method
    protected override Vector2[] GetCoveredNodes()
    {
        Vector2[] coveredNodes = new Vector2[6];

        int bottomLeftXPos = Mathf.RoundToInt(transform.position.x - 0.5f);
        int bottomLeftYPos = Mathf.RoundToInt(transform.position.y - 1f);

        int nodePlace = 0;

        for (int i = bottomLeftXPos; i < bottomLeftXPos + 2; i++)
        {
            for (int j = bottomLeftYPos; j < bottomLeftYPos + 3; j++)
            {
                coveredNodes[nodePlace] = new Vector2(i, j);
              
                nodePlace++;
            }
        }

        return coveredNodes;
    }
}
