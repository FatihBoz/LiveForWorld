using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveSpawn : MonoBehaviour
{
    public static WaveSpawn Instance { get; private set; }


    public WavesByPhase[] WaveInfos;
    private int currentPhase;
    public float phaseCooldown=30f;
    private float phaseTime;
    private int waveObjCount;
    public Transform player;
    public Enemy[] prefabs;
    public int spawnCount=10;
    public float distance;
    private float spawnTime=0;
    private float spawnCooldown=5f;
    [Header("player position + pos")]
    public Vector3[] pos;

    private float circleSpawnedTime;

    private float setWaveTime;

    private bool phaseFinished;

    public static Action OnLastWaveSpawned;
    private bool lastWaver;
    public static Action Wave5CountdownEnded;
    public float wave5Cooldown;
    private float wave5Time;
    private void Awake() 
    { 
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 


    }
    public void OnEnable()
    {
        OnLastWaveSpawned += LastWaveSpawn;
    }
    public void OnDisable()
    {
        OnLastWaveSpawned -= LastWaveSpawn;
    }
    void LastWaveSpawn()
    {
        lastWaver=true;
        wave5Time = Time.time;
    }
    void Start()
    {
        lastWaver=false;
        phaseFinished=false;
        phaseTime=Time.time;
        currentPhase=-1;
        // CircleWave circleWave = new CircleWave(player,prefabs[0],spawnCount,distance);
        // waveObjCount=circleWave.Spawn().Count;

        setWaveTime=Time.time;
        circleSpawnedTime=Time.time;
        spawnTime=Time.time;



/*         for (int i = 0; i < spawnCount; i++)
        {
            WaveObj spawnedObject = Instantiate(prefab);
            spawnedObject.target=player;
            float x = Mathf.Cos((2*Mathf.PI/spawnCount)*i)*distance;
            float z = Mathf.Sin((2*Mathf.PI/spawnCount)*i)*distance;
            spawnedObject.transform.position= new Vector3(x,transform.position.y,z);
        } */


    }

    public void Update()
    {
            if (Time.time>=wave5Time+wave5Cooldown && lastWaver)
            {
                lastWaver=false;
                Wave5CountdownEnded?.Invoke();
            }

            if (!phaseFinished && CheckAllWavesCleanInPhase())
            {
                phaseFinished=true;
                Debug.Log("bu faz bıtmıştır");
                phaseTime=Time.time;
            }

            if (Time.time>=phaseCooldown+phaseTime && phaseFinished)
            {
                // start
                currentPhase++;
                
                if (WaveInfos.Length>currentPhase)
                {
                    SetEnemyRates();
                }
                phaseFinished=false;
                
                if (currentPhase==4)
                {
                    OnLastWaveSpawned?.Invoke();
                }

                setWaveTime=Time.time;
                circleSpawnedTime=Time.time;
                spawnTime=Time.time;
            }

            if (currentPhase>=0 && WaveInfos.Length>currentPhase && !phaseFinished)
            {
              
                if (Time.time>=spawnTime+ WaveInfos[currentPhase].NormalWaveCooldown
                && WaveInfos[currentPhase].NormalWaveQuantity>0)
                {
                    spawnTime=Time.time;

                    Vector3 v3Pos = pos[Random.Range(0,pos.Length)];

                    Enemy spawnedObject = Instantiate(SelectEnemyByRatesInPhase());

                    spawnedObject.transform.position= v3Pos;

                    WaveInfos[currentPhase].NormalWaveQuantity--;
                }

                if (Time.time>=circleSpawnedTime+WaveInfos[currentPhase].CircleWaveCooldown
                && WaveInfos[currentPhase].CircleWaveQuantity>0)
                {
                    circleSpawnedTime=Time.time;
                    int inGameSpawnCount = Random.Range(spawnCount,30);

                    CircleWave circleWave1 = new CircleWave(player,SelectEnemyByRatesInPhase(),inGameSpawnCount,distance);
                    CircleWave circleWave2 = new CircleWave(player,SelectEnemyByRatesInPhase(),inGameSpawnCount,distance, (Mathf.PI/inGameSpawnCount));
                    circleWave1.Spawn();
                    circleWave2.Spawn();
                    
                    WaveInfos[currentPhase].CircleWaveQuantity--;
                }

                if (Time.time>=setWaveTime+WaveInfos[currentPhase].SetWaveCooldown
                && WaveInfos[currentPhase].SetWaveQuantity>0)
                {
                    setWaveTime=Time.time;
                    int rowCount=Random.Range(4,6);
                    SetWave setWave = new SetWave(
                        player,
                        SelectEnemyByRatesInPhase(),
                        rowCount*5,
                        rowCount,
                        pos[Random.Range(0,pos.Length)]
                        );    
                    setWave.Spawn();
                    WaveInfos[currentPhase].SetWaveQuantity--;
                    
                }
              
            }
            
    }
    public void DecreaseWaveObjCount()
    {
        waveObjCount--;
    }
    private bool CheckAllWavesCleanInPhase()
    {
        return currentPhase<0||(WaveInfos[currentPhase].CircleWaveQuantity<=0 
        && WaveInfos[currentPhase].SetWaveQuantity<=0
        && WaveInfos[currentPhase].NormalWaveQuantity<=0);
    }
    private void SetEnemyRates()
    {
  
        float sum=0;
        for (int i = 0; i < WaveInfos[currentPhase].enemyRates.Length; i++)
        {
            sum+=WaveInfos[currentPhase].enemyRates[i];
        }

        for (int i = 0; i < WaveInfos[currentPhase].enemyRates.Length; i++)
        {
        
           WaveInfos[currentPhase].enemyRates[i]/=sum;
           if (i>0)
           {
            WaveInfos[currentPhase].enemyRates[i]+=WaveInfos[currentPhase].enemyRates[i-1];
           }

        }
    }
    private Enemy SelectEnemyByRatesInPhase()
    {
        float randomVal=Random.Range(0.001f,1f);

        for (int i = 0; i < WaveInfos[currentPhase].enemyRates.Length; i++)
        {
           if (randomVal<=WaveInfos[currentPhase].enemyRates[i])
           {
                return prefabs[i];
           }
        }



        return prefabs[0];

    }

    public bool GetPhaseStatus()
    {
        return phaseFinished;
    }

}

[Serializable]
public class WavesByPhase
{
    public int SetWaveQuantity;
    public float SetWaveCooldown;
    public int CircleWaveQuantity;
    public float CircleWaveCooldown;
    public int NormalWaveQuantity;
    public float NormalWaveCooldown;

    public float[] enemyRates;
}