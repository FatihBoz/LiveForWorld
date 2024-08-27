using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    float ore = 100;

    float mainBuildingHealth = 500;

    float playerHealth = 200;

    public static PlayerProperties Instance;

    public TextMeshProUGUI OreText;

    private void Start()
    {
        UpdateText();
    }

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

    public void ChangeOreAmount(float amount)
    {
        ore += amount;
        UpdateText();
    }

    void UpdateText()
    {
        OreText.text = ore.ToString();

    }

    public float getOre()
    {
        return ore;
    }
    public float DecreasePlayerHealth(float amount)
    {
        playerHealth-=amount;
        if(playerHealth<=0)
        {
            Debug.Log(playerHealth);
        }
        return playerHealth;
    }
}
