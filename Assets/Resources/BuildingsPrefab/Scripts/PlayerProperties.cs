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
    public TMP_Text mobBloodText;
    private AudioSource audioSource;
    public AudioClip onHitSoundFx;
    public GameObject damageImageUI;
    public Material playerMat;
    public Slider healthBar;
    private float mobBloodCount;
    private void Start()
    {
        audioSource=GetComponent<AudioSource>();
        UpdateText();
        healthBar.maxValue=playerHealth;
        healthBar.value=playerHealth;
        mobBloodCount=0;
        mobBloodText.text=mobBloodCount.ToString();
    }
    public float IncreaseMobBloodCount(float value)
    {
        mobBloodCount+=value;
        mobBloodText.text=mobBloodCount.ToString();
        return mobBloodCount;
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
        healthBar.value=playerHealth;
        if(playerHealth<=0)
        {
            Debug.Log(playerHealth);
        }
        return playerHealth;
    }
}
