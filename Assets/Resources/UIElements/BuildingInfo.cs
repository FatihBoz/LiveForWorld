using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildingInfo : MonoBehaviour
{
    public TextMeshProUGUI buildingName;

    public TextMeshProUGUI buildingCost;

    public TextMeshProUGUI buildingLevel;

    public static BuildingInfo Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Oyun boyunca saklanmas�n� sa�la
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        this.gameObject.SetActive(false);
    }



    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        this.gameObject.SetActive(true);
    //        UpdateText();
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        this.gameObject.SetActive(false);
    //    }
    //}

    public void UpdateText(GameObject building)
    {
        buildingName.text = building.GetComponent<Building>().name;
        buildingCost.text = building.GetComponent<Building>().cost.ToString();
        buildingLevel.text = building.GetComponent <Building>().level.ToString();

    }
}
