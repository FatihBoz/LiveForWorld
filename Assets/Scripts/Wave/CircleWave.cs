using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleWave : Wave
{

    private float distance;
    private float startDegree;
    public CircleWave(Transform target, Enemy prefab,int count,float distance) : base(target,prefab,count)
   {
    this.distance=distance;
    this.startDegree=0;
   }
   public CircleWave(Transform target, Enemy prefab,int count,float distance,float startDegree) : base(target,prefab,count)
   {
    this.distance=distance;
    this.startDegree=startDegree;
   }

public override List<Enemy> Spawn() 
    {
        List<Enemy> spawnedEntities=new();
        for (int i = 0; i < Count; i++)
        {
            Enemy spawnedWaveObj=Object.Instantiate(Prefab);
            spawnedWaveObj.SetTarget(Target);
            float x = Target.position.x+Mathf.Cos(startDegree+(i*2*Mathf.PI/Count))*distance;
            float z = Target.position.z+Mathf.Sin(startDegree+(i*2*Mathf.PI/Count))*distance;
            spawnedWaveObj.transform.position=new Vector3(x,1f,z);
            spawnedEntities.Add(spawnedWaveObj);
        }
        return spawnedEntities;

    }
}
