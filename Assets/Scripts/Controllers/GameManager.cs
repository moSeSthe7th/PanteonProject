using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    public GameObject carriedBuilding;
    public GameObject selectedBuilding;
    public GameObject selectedSoldier;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

        DataScript.gridSizeX = 50;
        DataScript.gridSizeY = 50;
        DataScript.cameraMoveSpeed = 5f;

        DataScript.Barracks = ObjectPooler.instance.PooltheObjects(Resources.Load<GameObject>("Prefabs/Barracks"), 50);
        DataScript.PowerPlants = ObjectPooler.instance.PooltheObjects(Resources.Load<GameObject>("Prefabs/PowerPlant"), 50);
        DataScript.Soldiers = ObjectPooler.instance.PooltheObjects(Resources.Load<GameObject>("Prefabs/Soldier"), 100);
    }

    private void Update()
    {
        //if there is a selected building at the pointer
        if (carriedBuilding != null)
        {
            //make the information UI empty
            UIController.instance.MakeEmpty();
            Building building = carriedBuilding.GetComponent<Building>();


            if (Input.GetMouseButtonDown(0))
            {
                //if mouse is not on the UI elements
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    //Put building to a current location if not occupied
                    if (building.TryToPut())
                    {
                        carriedBuilding = null;
                    }
                }
            }
            //remove the carried building
            else if (Input.GetMouseButtonDown(1))
            {
                carriedBuilding.SetActive(false);
                carriedBuilding = null;
            }
            //carry the building
            else
            {
                building.CarryTheBuilding(GetMousePos());
            }
            
        }
        else if (Input.GetMouseButton(0))
        {
            //check for mouse clicks on items
            if (!EventSystem.current.IsPointerOverGameObject())
            {

                RaycastHit2D hit = Physics2D.Raycast(GetMousePos(), Vector2.zero);
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.tag == "Tile")
                    {
                        MoveTheCamera();
                    }
                    else if(hit.collider.gameObject.tag == "Barracks")
                    {
                        if (selectedSoldier != null)
                        {
                            selectedSoldier.GetComponent<SpriteRenderer>().color = Color.white;
                            selectedSoldier = null;
                        }
                            
                        if (selectedBuilding != null)
                        {
                            selectedBuilding.GetComponent<SpriteRenderer>().color = Color.white;
                            selectedBuilding = null;
                        }
                        UIController.instance.BarracksSelected();
                        selectedBuilding = hit.collider.gameObject;
                        selectedBuilding.GetComponent<SpriteRenderer>().color = Color.gray;
                    }
                    else if(hit.collider.gameObject.tag == "PowerPlant")
                    {
                        if (selectedSoldier != null)
                        {
                            selectedSoldier.GetComponent<SpriteRenderer>().color = Color.white;
                            selectedSoldier = null;
                        }

                        if (selectedBuilding != null)
                        {
                            selectedBuilding.GetComponent<SpriteRenderer>().color = Color.white;
                            selectedBuilding = null;
                        }
                        UIController.instance.PowerPlantSelected();
                        selectedBuilding = hit.collider.gameObject;
                        selectedBuilding.GetComponent<SpriteRenderer>().color = Color.gray;
                    }
                    else if(hit.collider.gameObject.tag == "Soldier")
                    {
                        if (selectedSoldier != null)
                        {
                            selectedSoldier.GetComponent<SpriteRenderer>().color = Color.white;
                            selectedSoldier = null;
                        }

                        if (selectedBuilding != null)
                        {
                            selectedBuilding.GetComponent<SpriteRenderer>().color = Color.white;
                            selectedBuilding = null;
                        }

                        selectedSoldier = hit.collider.gameObject;
                        selectedSoldier.GetComponent<SpriteRenderer>().color = Color.gray;
                    }
                }
            }
        }
        //move the selected soldier (if any) to mouse position with right click
        else if(Input.GetMouseButtonDown(1) && selectedSoldier != null && selectedBuilding == null)
        {
            selectedSoldier.GetComponent<Soldier>().MoveToNewPos(GetMousePos());
        }
    }


    void MoveTheCamera()
    {
        float translationX = Input.GetAxis("Mouse X") * DataScript.cameraMoveSpeed * Time.deltaTime;
        float translationY = Input.GetAxis("Mouse Y") * DataScript.cameraMoveSpeed * Time.deltaTime;

        if(Camera.main.transform.position.x+translationX<34f&& Camera.main.transform.position.x + translationX>15f
            &&Camera.main.transform.position.y + translationY<39f && Camera.main.transform.position.y + translationY > 10f)
                Camera.main.transform.Translate(new Vector3(translationX, translationY, 0));
    }

    public Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void CreateSoldier()
    {
        if (selectedBuilding != null)
        {
            if (selectedBuilding.GetComponent<Barracks>() != null)
            {
                selectedBuilding.GetComponent<Barracks>().SpawnSoldier();
            }
        }
    }
}
