using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Building
{
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
        if (productionTimer >= level)
        {
            ProduceResource();
            productionTimer = 0f; // Zamanlayýcýyý sýfýrla
        }

        if (Input.GetKeyDown(KeyCode.Q) && isPressed)
        {
            UpgradeBuilding();
        }
    }
    /*
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Valla player tespit ettim");
            isPressed = true;
            //BuildUI.SetActive(true);

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Valla player tespit ettim");
            isPressed = false;
            //BuildUI.SetActive(true);

        }
    }
    */
    void ProduceResource()
    {
        // Her üretimde oyuncu kaynaklarýný artýr
        PlayerProperties.Instance.ChangeOreAmount(level); // Saniyede 1 artýr
    }

    public override void UpgradeBuilding()
    {
        if (level < 3 && PlayerProperties.Instance.getOre() > cost)
        {
            PlayerProperties.Instance.ChangeOreAmount(-cost);

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
