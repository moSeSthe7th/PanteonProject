using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputAction : MonoBehaviour
{
    public GameObject content;
    List<RectTransform> barracksTransforms;
    List<RectTransform> powerPlantTransforms;
    RectTransform contentRect;
   
    bool isInitialized;
    float worldSizeOfContentRect;
    float rectYSizeOfTransforms;

    private void Start()
    {
        rectYSizeOfTransforms = content.GetComponent<ProductionContent>().rectYSizeOfProducts;
        barracksTransforms = new List<RectTransform>();
        powerPlantTransforms = new List<RectTransform>();
        isInitialized = false;
        contentRect = content.GetComponent<RectTransform>();
    }

    


    //Update content of the scroll view when scrolled
    void UpdateContent()
    {
        //initialize at the first movement of the scroll to increase performance
        if (!isInitialized)
        {

            isInitialized = true;
            //set barrack and power plant transforms
            foreach (GameObject barrack in DataScript.barracksButtons)
                barracksTransforms.Add(barrack.GetComponent<RectTransform>());
            foreach (GameObject pp in DataScript.powerPlantButtons)
                powerPlantTransforms.Add(pp.GetComponent<RectTransform>());

            //find the size of the content rect i will use that to understand the relation with rect transform of the objects
            //when i found the relation i will determine when the scroll is exactly same with the size 
            Vector3[] topBarrackWorldPoints = new Vector3[4];
            Vector3[] bottomBarrackWorldPoints = new Vector3[4];
            barracksTransforms[10].GetWorldCorners(topBarrackWorldPoints);
            barracksTransforms[5].GetWorldCorners(bottomBarrackWorldPoints);
            worldSizeOfContentRect = topBarrackWorldPoints[0].y - bottomBarrackWorldPoints[0].y;
        }

        //found the content's relative position to use in updating
        float x = contentRect.localPosition.y / (worldSizeOfContentRect * rectYSizeOfTransforms * -1f);
        int contentRelativePos;
        if (x < 0)
            contentRelativePos = Mathf.FloorToInt(x);
        else
            contentRelativePos = Mathf.CeilToInt(x);

        //check if rect transforms outsize too far to the center and if a transform is too far rearrange all of the content
        foreach (RectTransform barracksRect in barracksTransforms)
        {
            if (barracksRect.anchorMax.y > (contentRelativePos + 5) * rectYSizeOfTransforms)
            {
                content.GetComponent<ProductionContent>().CreateProductionMenu(contentRelativePos * rectYSizeOfTransforms - 1.0f);
                
            }
            if(barracksRect.anchorMin.y < (contentRelativePos - 5) * rectYSizeOfTransforms)
            {
                content.GetComponent<ProductionContent>().CreateProductionMenu(contentRelativePos * rectYSizeOfTransforms + 1.0f);
            }
        }
    }
}
