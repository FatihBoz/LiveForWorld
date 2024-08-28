using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mine : Building //TODO: LEVEL ATLAYINCA DAHA FAZLA PARA VER
{
    private float productionTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if(level == 1)
        {

            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y+3f, this.transform.position.z);

            // Mevcut GameObject'in rotasyonunu g�ncelle
            this.transform.rotation = Quaternion.Euler(-90, 0, 0);

        }
        //InputManager.Instance.input.Player.ActivateBuilding.performed += UpgradeBuilding;

    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        
        // Zamanlay�c�y� g�ncelle
        productionTimer += Time.deltaTime;

        // Zamanlay�c� belirlenen aral��a ula�t�ysa �retimi ger�ekle�tir
        if (productionTimer >= level && health>0)
        {
            ProduceResource();
            productionTimer = 0f; // Zamanlay�c�y� s�f�rla
        }

        if (Input.GetKeyDown(KeyCode.Q) && isPressed)
        {
            UpgradeBuilding();
        }
    }
   
    void ProduceResource()
    {
        // Her �retimde oyuncu kaynaklar�n� art�r
        PlayerProperties.Instance.ChangeOreAmount(level); // Saniyede 1 art�r
    }

    public override void UpgradeBuilding()
    {
        if (PlayerProperties.Instance.getOre() < cost)
        {
            Debug.Log("Not Enough Ore");
            return;
        }

        if (level <3)
        {
            PlayerProperties.Instance.ChangeOreAmount(-cost);


            health += 50;

            maxHealth += 50;

            level++;

            GameObject temp = Instantiate(BuildingsPrefab.Instance.mineLevels[level-1],this.transform.position, this.transform.rotation);


            temp.GetComponent<Building>().level = level;


            Destroy(this.gameObject);

        }
        else
        {
            Debug.Log("Bina zaten maksimum seviyede. : " + level);
        }
    }

}
