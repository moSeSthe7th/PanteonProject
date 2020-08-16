using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProductionContent : MonoBehaviour
{
    GameObject barracksButton;
    GameObject powerPlantButton;
    RectTransform rectTransform;
    public float rectYSizeOfProducts;

    void Start()
    {
        // Y size of products 
        rectYSizeOfProducts = 0.2f;
        rectTransform = GetComponent<RectTransform>();

        //object pooling the buttons we will use 11 buttons for infinite scroll
        barracksButton = Resources.Load<GameObject>("Prefabs/UIPrefabs/BarracksScrollBtn");
        powerPlantButton = Resources.Load<GameObject>("Prefabs/UIPrefabs/PowerPlantScrollButton");
        DataScript.barracksButtons = ObjectPooler.instance.PooltheObjects(barracksButton, 11, gameObject.transform,true);
        DataScript.powerPlantButtons = ObjectPooler.instance.PooltheObjects(powerPlantButton, 11, gameObject.transform,true);
        
        //Create menu starting from Y=-1
        CreateProductionMenu(-1f);
        
    }

    //Creating the menu with the given bottom Y value.
    public void CreateProductionMenu(float startY)
    {

        //Upper right and lower left points of barrack buttons
        Vector2 lowerLeftBarracks = new Vector2(0f, startY);
        Vector2 upperRightBarracks = new Vector2(0.4f, startY + rectYSizeOfProducts);

        for (int i = 0; i < DataScript.barracksButtons.Count; i++)
        {

            //initialize button
            DataScript.barracksButtons[i].GetComponent<RectTransform>().anchorMax = upperRightBarracks;
            DataScript.barracksButtons[i].GetComponent<RectTransform>().anchorMin = lowerLeftBarracks;

            //increase y values for next button
            lowerLeftBarracks.y += rectYSizeOfProducts;
            upperRightBarracks.y += rectYSizeOfProducts;
        }

        //Upper right and lower left points of power plant buttons
        Vector2 lowerLeftPowerPlant = new Vector2(0.6f, startY);
        Vector2 upperRightPowerPlant = new Vector2(1f, startY + rectYSizeOfProducts);

        for (int i = 0; i < DataScript.powerPlantButtons.Count; i++)
        {
            //initialize button
            DataScript.powerPlantButtons[i].GetComponent<RectTransform>().anchorMax = upperRightPowerPlant;
            DataScript.powerPlantButtons[i].GetComponent<RectTransform>().anchorMin = lowerLeftPowerPlant;

            //increase y values for next button
            lowerLeftPowerPlant.y += rectYSizeOfProducts;
            upperRightPowerPlant.y += rectYSizeOfProducts;
        }


    }

   
   
}
