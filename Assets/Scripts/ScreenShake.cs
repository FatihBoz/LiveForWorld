using System.Collections;
using UnityEngine;
using Cinemachine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance { get; private set; }    

    private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin perlinNoise;

    public float shakeDuration = 0.5f;
    public float shakeAmplitude = 2.0f; // Intensity of the shake
    public float shakeFrequency = 2.0f; // Speed of the shake

    private float shakeTimer;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        perlinNoise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void TriggerShake()
    {
        perlinNoise.m_AmplitudeGain = shakeAmplitude;
        perlinNoise.m_FrequencyGain = shakeFrequency;

        shakeTimer = shakeDuration;
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;

            //reduce shake over time
            if (shakeTimer <= 0f)
            {
                perlinNoise.m_AmplitudeGain = 0f;
                perlinNoise.m_FrequencyGain = 0f;
            }
        }
    }
}