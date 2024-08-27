using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProperties : MonoBehaviour
{
    float ore = 100;

    float mainBuildingHealth = 500;

    float playerHealth = 200;

    public static PlayerProperties Instance;

    public TextMeshProUGUI OreText;
    private AudioSource audioSource;
    public AudioClip onHitSoundFx;
    public Image damageImageUI;
    public Material playerMat;
    private bool redded;

    private void Start()
    {
        redded=false;
        audioSource=GetComponent<AudioSource>();
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
    public void Update()
    {
            playerMat.color=Color.Lerp(playerMat.color,Color.white,Time.deltaTime*5);

    }
    public float getOre()
    {
        return ore;
    }
    public float DecreasePlayerHealth(float amount)
    {
        ScreenShake.Instance.TriggerShake();
        playerMat.color=Color.red;
        audioSource.PlayOneShot(onHitSoundFx);
        damageImageUI.gameObject.SetActive(true);
        playerHealth-=amount;
        if(playerHealth<=0)
        {
            Debug.Log(playerHealth);
        }
        return playerHealth;
    }
}
