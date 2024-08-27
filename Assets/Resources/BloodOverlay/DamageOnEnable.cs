using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnEnable : MonoBehaviour
{
    private float hexa=5f;
    private void OnEnable()
    {
        hexa=5f;

    }
    void Update()
    {
        hexa-=Time.deltaTime;
        if (hexa<=0)
        {
            gameObject.SetActive(false);
        }
    }
}
