using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSoldierButton : MonoBehaviour
{
    public void CreateSoldier()
    {
        GameManager.instance.CreateSoldier();
    }
}
