using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    private Pathfinding pathfinding;

    //lock input while moving to prevent unwanted behaviour
    bool lockInput;

    private void Start()
    {
        lockInput = false;
        pathfinding = GetComponent<Pathfinding>();
    }

    // calculate the path and move the soldier along the path
    public void MoveToNewPos(Vector2 pos)
    {
        if (!lockInput)
        {
            pos.x = Mathf.RoundToInt(pos.x);
            pos.y = Mathf.RoundToInt(pos.y);
            //start movement if target pos is not occupied
            if (!Grid.instance.grid[(int)pos.x, (int)pos.y].isOccupied)
            {
                //lock the input
                lockInput = true;
                List<Node> path = new List<Node>();

                path = pathfinding.FindPath((int)transform.position.x, (int)transform.position.y, (int)pos.x, (int)pos.y);
                StartCoroutine(WalkOnPath(path));
            }
        }
       
    }

    //walk through path
    IEnumerator WalkOnPath(List<Node> path)
    {
        Vector2 targetPos;

        //select path nodes one by one
        if(path != null)
        {
            for (int i = 0; i < path.Count; i++)
            {
                targetPos = path[i].position;
                //walk to the next path node
                while (Vector2.Distance(transform.position, targetPos) > 0.3f)
                {
                    transform.position = Vector2.MoveTowards(transform.position, targetPos, 0.1f);
                    yield return new WaitForEndOfFrame();
                }
                transform.position = targetPos;
            }
        }
        
        lockInput = false;
    }
}
