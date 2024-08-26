using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawn : MonoBehaviour
{
    

    public Transform player;
    public WaveObj prefab;
    public int spawnCount=10;
    public float distance;
    void Start()
    {
        
        for (int i = 0; i < spawnCount; i++)
        {
            WaveObj spawnedObject = Instantiate(prefab);
            spawnedObject.target=player;
            float x = Mathf.Cos((2*Mathf.PI/spawnCount)*i)*distance;
            float z = Mathf.Sin((2*Mathf.PI/spawnCount)*i)*distance;
            spawnedObject.transform.position= new Vector3(x,transform.position.y,z);
        }

    }

}
