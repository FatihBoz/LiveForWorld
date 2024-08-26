using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    float ore;

    public static PlayerProperties Instance;


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

    public void ChangeOreAmount(float amount)
    {
        ore += amount;
    }
}
