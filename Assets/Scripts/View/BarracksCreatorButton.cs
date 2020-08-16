using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarracksCreatorButton : MonoBehaviour
{
   public void CreateBarracks()
    {
        if(GameManager.instance.carriedBuilding == null)
        {
            GameObject barrack = ObjectPooler.instance.GetPooledObject(DataScript.Barracks);
            barrack.SetActive(true);
            GameManager.instance.carriedBuilding = barrack;
        }
       
    }
}
