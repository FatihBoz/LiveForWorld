using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawn : MonoBehaviour
{
    public static WaveSpawn Instance { get; private set; }
    private int waveObjCount;
    public Transform player;
    public WaveObj prefab;
    public int spawnCount=10;
    public float distance;
    private float spawnTime=0;
    private float spawnCooldown=5f;
    private Vector3[] pos;
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
    void Start()
    {
        pos=new Vector3[]
        {new Vector3(1.5f, 1.2f, 10f),
            new Vector3( 0, 0f, 10f)
        ,new Vector3(-0.5f, 1.2f, 10f)
        ,new Vector3(1.2f, 0f, 10f)
        };

        CircleWave circleWave = new CircleWave(player,prefab,spawnCount,distance);
        waveObjCount=circleWave.Spawn().Count;
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
        if (waveObjCount<=75)
        {
            if (Time.time>=spawnCooldown+spawnTime)
            {
                spawnTime=Time.time;
                Vector3 v3Pos = Camera.main.ViewportToWorldPoint(pos[Random.Range(0,pos.Length)]);
                v3Pos.y=1f;
                WaveObj spawnedObject = Instantiate(prefab);
                spawnedObject.SetTarget(player);
                spawnedObject.transform.position= v3Pos;
                waveObjCount++;
            }
            
        }
    }
    public void DecreaseWaveObjCount()
    {
        waveObjCount--;
    }
}
