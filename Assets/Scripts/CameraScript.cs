using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Vector3 cameraStartPos;
    //x 15/34
    //y 10/39

    void Start()
    {
        //set the start position of the camera and ortographic size
        cameraStartPos = new Vector3(Mathf.RoundToInt(DataScript.gridSizeX / 2), Mathf.RoundToInt(DataScript.gridSizeY / 2),-10f);
        //Camera.main.orthographicSize = Screen.height/500f;
        Camera.main.transform.position = cameraStartPos;
    }

}
