using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWave : Wave
{
    private Vector3 baseCoordinate;
    private int rowCount;
    public SetWave(Transform target, Enemy prefab, int count,int rowCount,Vector3 baseCoordinate) : base(target, prefab, count)
    {
        this.baseCoordinate=baseCoordinate;
        baseCoordinate.y=0;
        this.rowCount=rowCount;
    }

    public override List<Enemy> Spawn()
    {
        List<Enemy> spawnedEnemies = new();
        for (int i = 0; i < Count; i++)
        {
            Enemy spawnedWaveObj=Object.Instantiate(Prefab);
           // spawnedWaveObj.SetTarget(Target);
            float x = 1+i%rowCount;
            float z = 1+i/rowCount;
            spawnedWaveObj.transform.position=new Vector3(x,1f,z)+baseCoordinate;
            spawnedEnemies.Add(spawnedWaveObj);
        }
        return spawnedEnemies;

    }
}
