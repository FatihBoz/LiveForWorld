using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingInfo : MonoBehaviour
{
    public TextMeshProUGUI buildingName;

    public TextMeshProUGUI buildingCost;

    public TextMeshProUGUI buildingLevel;

    public static BuildingInfo Instance;

    public Slider buildingHealth;



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

    public void AssignHealth(GameObject building)
    {
        buildingHealth.maxValue = building.GetComponent<Building>().maxHealth;
        buildingHealth.value = building.GetComponent<Building>().health;
    }

    public void UpdateHealth(GameObject building)
    {
        buildingHealth.value = buildingHealth.value = building.GetComponent<Building>().health;
    }

    public void UpdateText(GameObject building)
    {
        buildingName.text = building.GetComponent<Building>().name;
        buildingCost.text = building.GetComponent<Building>().cost.ToString();
        buildingLevel.text = building.GetComponent <Building>().level.ToString();

    }
}
