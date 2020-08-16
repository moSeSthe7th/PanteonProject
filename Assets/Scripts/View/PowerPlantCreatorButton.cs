using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlantCreatorButton : MonoBehaviour
{
    public void CreatePowerPlant()
    {
        if(GameManager.instance.carriedBuilding == null)
        {
            GameObject powerPlant = ObjectPooler.instance.GetPooledObject(DataScript.PowerPlants);
            powerPlant.SetActive(true);
            GameManager.instance.carriedBuilding = powerPlant;
        }
       
    }
}
