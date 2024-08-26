using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionSite : MonoBehaviour
{
    public bool isActive = false;
    public GameObject buildingPrefabName;




    private void ActivateBuilding()
    {
        GameObject buildingPrefab = Resources.Load<GameObject>("Buildings/" + buildingPrefabName);
        isActive = true;
        Instantiate(buildingPrefab, transform.position, Quaternion.identity);
        
    }
}
