using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhaseCountdown : MonoBehaviour
{
    public GameObject phaseMainObj;
    public TMP_Text countdown;
    public float thatsCountdown;
    void Start()
    {
        thatsCountdown=WaveSpawn.Instance.phaseCooldown;
        countdown.text=thatsCountdown.ToString();
    }

    void Update()
    {
        if (!WaveSpawn.Instance.GetPhaseStatus())
        {
            thatsCountdown=WaveSpawn.Instance.phaseCooldown;
            phaseMainObj.SetActive(false);
        }
        else
        {
            phaseMainObj.SetActive(true);
        }
        thatsCountdown-=Time.deltaTime;
        countdown.text=((int)thatsCountdown).ToString();
    }
}
