using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Building
{
    // Start is called before the first frame update
    void Start()
    {
        this.level = 1;
    }

    // Update is called once per frame
    void Update()
    {
        ProduceResource();
    }

    void ProduceResource()
    {
        // Zamanla �retimi art�r
        PlayerProperties.Instance.ChangeOreAmount(level * Time.deltaTime);
    }



    void ChangeBuildingProperties()
    {

    }
}
