using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance = null;

    public Sprite barracksImage;
    public Sprite powerPlantImage;
    public Sprite soldierImage;

    public Text infoText;
    public Image buildingImg;
    public Button productBtn;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

        MakeEmpty();
    }

    
    //No selected buildings
    public void MakeEmpty()
    {
        infoText.gameObject.SetActive(false);
        buildingImg.gameObject.SetActive(false);
        productBtn.gameObject.SetActive(false);
    }

    
    public void BarracksSelected()
    {
        infoText.gameObject.SetActive(true);
        buildingImg.gameObject.SetActive(true);
        productBtn.gameObject.SetActive(true);

        infoText.text = "Barracks";
        buildingImg.sprite = barracksImage;
        productBtn.image.sprite = soldierImage;
    }

    public void PowerPlantSelected()
    {
        infoText.gameObject.SetActive(true);
        buildingImg.gameObject.SetActive(true);
        productBtn.gameObject.SetActive(false);

        infoText.text = "Power Plant";
        buildingImg.sprite = powerPlantImage;
       
    }
}
