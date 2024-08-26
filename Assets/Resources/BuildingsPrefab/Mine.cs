using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Building
{
    
    // Start is called before the first frame update
    void Start()
    {
        level = 1;

    }

    // Update is called once per frame
    void Update()
    {
        ProduceResource();
    }

    void ProduceResource()
    {
        // Zamanla üretimi artýr
        PlayerProperties.Instance.ChangeOreAmount(level * Time.deltaTime);
    }
    
    public override void UpgradeBuilding()
    {
        if (level < 3)
        {
            PlayerProperties.Instance.ChangeOreAmount(cost);

            level++;

            health += 50;
            Debug.Log("Maden seviyesi yükseltildi! Yeni seviye: " + level);
        }
        else
        {
            Debug.Log("Bina zaten maksimum seviyede.");
        }
    }

}
