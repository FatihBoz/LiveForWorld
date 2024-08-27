using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsPrefab : MonoBehaviour
{
    public static BuildingsPrefab Instance;

    public GameObject[] turretLevels = new GameObject[2];

    public GameObject[] mineLevels = new GameObject[2];

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Oyun boyunca saklanmasýný saðla
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
