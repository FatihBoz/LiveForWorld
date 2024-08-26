using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Building
{
    public float productionInterval = 1f;
    private float productionTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        level = 1;

        //InputManager.Instance.input.Player.ActivateBuilding.performed += UpgradeBuilding;

    }

    // Update is called once per frame
    void Update()
    {
        // Zamanlayýcýyý güncelle
        productionTimer += Time.deltaTime;

        // Zamanlayýcý belirlenen aralýða ulaþtýysa üretimi gerçekleþtir
        if (productionTimer >= productionInterval)
        {
            ProduceResource();
            productionTimer = 0f; // Zamanlayýcýyý sýfýrla
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            UpgradeBuilding();
        }
    }

    void ProduceResource()
    {
        // Her üretimde oyuncu kaynaklarýný artýr
        PlayerProperties.Instance.ChangeOreAmount(1f); // Saniyede 1 artýr
    }

    public override void UpgradeBuilding()
    {
        if (level < 3 && PlayerProperties.Instance.getOre() > cost)
        {
            PlayerProperties.Instance.ChangeOreAmount(cost);

            level++;

            health += 50;

            maxHealth += 50;

            Debug.Log("Maden seviyesi yükseltildi! Yeni seviye: " + level);
        }
        else
        {
            Debug.Log("Bina zaten maksimum seviyede.");
        }
    }

}
